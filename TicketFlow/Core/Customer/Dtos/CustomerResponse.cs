﻿namespace TicketFlow.Core.Customer.Dtos;

public record CustomerResponse
{
    public Guid Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }
}