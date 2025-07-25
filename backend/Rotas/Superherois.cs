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
                return Results.Created($"Superheroi {heroi.NomeHeroi} criado com sucesso!", heroi);
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
            var herois = await db.Herois.ToListAsync();
            
            return herois == null || herois.Count == 0
                ? Results.NotFound($"Superherois não encontrados")
                : Results.Ok(herois);
        });

        app.MapGet("/lista_heroi/{id}", async (int HeroiId, ViceriTesteTecnicoContext db) =>
        {
            var heroi = await db.Herois.FindAsync(HeroiId);

            return heroi is null
                ? Results.NotFound($"Heroi com o id {HeroiId} não encontrado")
                : Results.Ok(heroi);
        });

        app.MapPut("/atualiza_heroi/{id}", async (int HeroiId, HeroiDTO heroiDto, ViceriTesteTecnicoContext db) =>
        {
            Heroi heroi_existente = await db.Herois.FindAsync(HeroiId);

            if (heroi_existente == null) return Results.Conflict($"Herói de id:{HeroiId} ainda não existe no banco de dados.");

            heroi_existente.Nome = heroiDto.Nome;
            heroi_existente.NomeHeroi = heroiDto.NomeHeroi;
            heroi_existente.DataNascimento = heroiDto.DataNascimento;
            heroi_existente.Altura = heroiDto.Altura;
            heroi_existente.Peso = heroiDto.Peso;

            try
            {
                await db.SaveChangesAsync();
                return Results.Ok($"Heroi de id:{HeroiId} atualizado com sucesso");
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

        app.MapDelete("/deletar_heroi/{id}", async (int HeroiId, ViceriTesteTecnicoContext db) =>
        {
            var heroi = await db.Herois.FindAsync(HeroiId);
            if (heroi == null) return Results.NotFound($"Heroi de id:{HeroiId} nao encontrado no banco de dados");

            db.Herois.Remove(heroi);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });

    }
}