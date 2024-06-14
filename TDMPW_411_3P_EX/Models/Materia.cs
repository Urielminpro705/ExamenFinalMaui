namespace TDMPW_411_3P_EX.Models;

public class Materia
{
    public string Nombre { get; set; }
    public string[] Rubros { get; set; }
    public double[] ValorRubros { get; set; }
    public double[] Calificaciones { get; set; }
    public double CalificacionTotal { get; set; }
}
