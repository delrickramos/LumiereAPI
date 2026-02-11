using System;
using System.Collections.Generic;
using System.Text;

namespace Lumiere.Models
{
    public enum StatusIngressoEnum
    {
        Reservado = 0, // TODO: Para implementação futura - Fluxo de pagamento e expiração de reservas
        Cancelado = 1, // TODO: Para implementação futura - Endpoint PUT /ingressos/{id}/cancelar
        Confirmado = 2
    }
}
