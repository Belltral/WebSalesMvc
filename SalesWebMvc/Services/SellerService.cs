using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); //Síncrona
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj); //Adiciona os itens do formulário no banco
            _context.SaveChanges(); //Salva as adições
        }
    }
}
