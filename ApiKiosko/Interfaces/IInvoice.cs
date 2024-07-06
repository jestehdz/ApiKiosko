using ApiKiosko.Models;

namespace ApiKiosko.Interfaces
{
    public interface IInvoice
    {
        Task<List<InvoiceHed>> GetInvoiceHed();
        Task<List<InvoiceDtl>> GetInvoiceDtl(int id);
        Task<String> PostCreateInvoiceHed(InvoiceHed invoiceHed);
        Task<string> PostCreateInvoiceDtl(int invoicenum);
    }
}
