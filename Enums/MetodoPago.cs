using System.ComponentModel;

namespace Backend_Gimnacio.Enums
{
    
    public enum MetodoPago
    {
        [Description("Efectivo")]
        Efectivo = 1,

        [Description("QR")]
        QR = 2,

        [Description("Transferencia")]
        Transferencia = 3,

        [Description("Tarjeta")]
        Tarjeta = 4
    }
}