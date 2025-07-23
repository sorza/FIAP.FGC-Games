using Fgc.Application.Compartilhado.Repositorios.Abstracoes;
using Fgc.Application.Usuario.CasosDeUso.Conta.Autenticar;
using Fgc.Domain.Usuario.Entidades;
using Fgc.Domain.Usuario.ObjetosDeValor;
using Fgc.Infrastructure.Compartilhado.Data;
using Fgc.Infrastructure.Compartilhado.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Fgc.Infrastructure.Usuario.Repositorios
{
    public class UsuarioRepository : GenericoRepository<Conta>, IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Conta?> Autenticar(Email email, CancellationToken cancellationToken)
        {
            var conta = await _context.Contas
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Address == email.Address, cancellationToken);

            return conta;
        }
            

        public bool VerificaSeUsuarioExiste(string email, CancellationToken cancellationToken = default)
            => _context.Contas.AsNoTracking().Any(u => u.Email.Address == email);
        
    }
}
