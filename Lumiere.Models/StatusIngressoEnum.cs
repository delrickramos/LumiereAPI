using System;
using System.Collections.Generic;
using System.Text;

namespace Lumiere.Models
{
    // Define o status do ingresso no sistema
    public enum StatusIngressoEnum
    {
        // Para implementação futura: fluxo de pagamento e expiração de reservas
        Reservado = 0,
        // Para implementação futura: endpoint PUT /ingressos/{id}/cancelar
        Cancelado = 1,
        Confirmado = 2
    }
}
