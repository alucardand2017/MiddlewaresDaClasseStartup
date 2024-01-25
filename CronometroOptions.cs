using System;

public class CronometroOptions
{
    public int qntdeUnidades = Enum.GetValues(typeof(UnidadesTempo)).Length;
    public UnidadesTempo UnidadeMedida {get;set;}
}