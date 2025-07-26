using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Microsoft.JSInterop.Infrastructure;

namespace backend.SuperheroisRotas;

public static class RotasSuperherois{
    public static void RotasSuperherois_(this WebApplication app){
        app.MapPost("/adiciona_superheroi", async (HeroiDTO heroiDto, ViceriTesteTecnicoContext db) =>
        {
            var heroi_existente = await db.Herois.FirstOrDefaultAsync(u => u.NomeHeroi == heroiDto.NomeHeroi);
            if (heroi_existente != null) return Results.Conflict("Herói já existe no banco de dados.");

            var poderes_existentes = await db.Superpoderes
                .Where(sp => heroiDto.Poderes.Contains(sp.Superpoder))
                .ToListAsync();

            if(poderes_existentes.Count != heroiDto.Poderes.Count) return Results.Conflict("Poderes não existentes no banco de dados.");

            var heroi = new Heroi
            {
                Nome = heroiDto.Nome,
                NomeHeroi = heroiDto.NomeHeroi,
                DataNascimento = heroiDto.DataNascimento,
                Altura = heroiDto.Altura,
                Peso = heroiDto.Peso
            };
            
            try
            {
                db.Herois.Add(heroi);
                await db.SaveChangesAsync();

                foreach (var p in poderes_existentes)
                {
                    Console.WriteLine($"HeroiId: {heroi.Id}, SuperpoderId: {p.Id}");
                    db.HeroisSuperpoderes.Add(new HeroisSuperpoderes 
                    { 
                        HeroiId = heroi.Id, 
                        SuperpoderId = p.Id 
                    });
                }
                await db.SaveChangesAsync();
                var heroiDtoCriado = new HeroiDTO
                {
                    Nome = heroi.Nome,
                    NomeHeroi = heroi.NomeHeroi,
                    DataNascimento = heroi.DataNascimento,
                    Altura = heroi.Altura,
                    Peso = heroi.Peso,
                    Poderes = poderes_existentes.Select(p => p.Superpoder).ToList()
                };
                return Results.Created($"Superheroi {heroi.NomeHeroi} criado com sucesso!", heroiDtoCriado);
            }
            catch (DbUpdateException ex)
            {
                return Results.Problem("Erro na base de dados:" + ex.InnerException?.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                return Results.Problem("Erro no servidor/backend:" + ex.Message, statusCode: 500);
            }
        });

        app.MapGet("/lista_superherois", async (ViceriTesteTecnicoContext db) =>
        {
            var herois = await db.Herois
                .Include(h => h.HeroisSuperpoderes)
                .ThenInclude(hs => hs.Superpoder)
                .ToListAsync();

            if (herois == null || herois.Count == 0)
                return Results.NotFound($"Superherois não encontrados");

            var heroisDto = herois.Select(u => new 
            {
                Id = u.Id,
                Nome = u.Nome,
                NomeHeroi = u.NomeHeroi,
                DataNascimento = u.DataNascimento,
                Altura = u.Altura,
                Peso = u.Peso,
                Poderes = u.HeroisSuperpoderes.Select(ui => ui.Superpoder.Superpoder).ToList()
            }).ToList();
            return Results.Ok(heroisDto);
        });

        app.MapGet("/lista_heroi/{id}", async (int id, ViceriTesteTecnicoContext db) =>
        {
            var heroi = await db.Herois
                .Include(u => u.HeroisSuperpoderes)
                .ThenInclude(ui => ui.Superpoder)
                .Where(u => u.Id == id)
                .Select(u => new 
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    NomeHeroi = u.NomeHeroi,
                    DataNascimento = u.DataNascimento,
                    Altura = u.Altura,
                    Peso = u.Peso,
                    Poderes = u.HeroisSuperpoderes.Select(ui => ui.Superpoder.Superpoder).ToList()
                })
                .FirstOrDefaultAsync();

            return heroi is null
                ? Results.NotFound($"Heroi com o id {id} não encontrado")
                : Results.Ok(heroi);
        });

        app.MapPut("/atualiza_heroi/{id}", async (int id, HeroiDTO heroiDto, ViceriTesteTecnicoContext db) =>
        {
            Heroi heroi_existente = await db.Herois.FindAsync(id);

            if (heroi_existente == null) return Results.Conflict($"Herói de id:{id} ainda não existe no banco de dados.");

            heroi_existente.Nome = heroiDto.Nome;
            heroi_existente.NomeHeroi = heroiDto.NomeHeroi;
            heroi_existente.DataNascimento = heroiDto.DataNascimento;
            heroi_existente.Altura = heroiDto.Altura;
            heroi_existente.Peso = heroiDto.Peso;

            try
            {
                // Remove poderes antigos
                var poderesAntigos = db.HeroisSuperpoderes.Where(hs => hs.HeroiId == id);
                db.HeroisSuperpoderes.RemoveRange(poderesAntigos);

                // Busca os novos poderes existentes
                var poderesNovos = await db.Superpoderes
                    .Where(sp => heroiDto.Poderes.Contains(sp.Superpoder))
                    .ToListAsync();

                if (poderesNovos.Count != heroiDto.Poderes.Count)
                    return Results.Conflict("Alguns poderes não existem no banco de dados.");

                // Adiciona os novos poderes
                foreach (var poder in poderesNovos)
                {
                    db.HeroisSuperpoderes.Add(new HeroisSuperpoderes
                    {
                        HeroiId = id,
                        SuperpoderId = poder.Id
                    });
                }

                await db.SaveChangesAsync();
                return Results.Ok($"Heroi de id:{id} atualizado com sucesso");
            }
            catch (DbUpdateException ex)
            {
                return Results.Problem("Erro na base de dados:" + ex.InnerException?.Message, statusCode: 400);
            }
            catch (Exception ex)
            {
                return Results.Problem("Erro no servidor/backend:" + ex.Message, statusCode: 500);
            }
        });

        app.MapDelete("/deletar_heroi/{id}", async (int id, ViceriTesteTecnicoContext db) =>
        {
            var heroi = await db.Herois
                .Include(h => h.HeroisSuperpoderes)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (heroi == null) return Results.NotFound($"Heroi de id:{id} nao encontrado no banco de dados");

            // Remove todos os vínculos de poderes
            db.HeroisSuperpoderes.RemoveRange(heroi.HeroisSuperpoderes);

            db.Herois.Remove(heroi);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

    }
}