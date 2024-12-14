﻿namespace OutfitTrack.Arguments;

public class OutputOrder
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ChangeDate { get; set; }
    public long? Number { get; set; }
    public long? CustomerId { get; set; }
    public EnumStatusOrder? Status { get; set; }
    public DateTime? ClosingDate { get; set; }
    public string? Observation { get; set; }

    public List<OutputOrderItem>? ListOrderItem { get; set; }
    public OutputCustomer? Customer { get; set; }
}