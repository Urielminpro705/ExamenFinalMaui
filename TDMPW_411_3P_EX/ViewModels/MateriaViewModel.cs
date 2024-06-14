using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TDMPW_411_3P_EX.Models;

namespace TDMPW_411_3P_EX.ViewModels;

public partial class MateriaViewModel : ObservableObject
{
    [ObservableProperty]
    private Materia _materia;

    [ObservableProperty]
    private string _nombre;

    [ObservableProperty]
    private string _rubro1;
    [ObservableProperty]
    private string _rubro2;
    [ObservableProperty]
    private string _rubro3;

    [ObservableProperty]
    private string _val1;
    [ObservableProperty]
    private string _val2;
    [ObservableProperty]
    private string _val3;

    [ObservableProperty]
    private string _cal1;
    [ObservableProperty]
    private string _cal2;
    [ObservableProperty]
    private string _cal3;

    public MateriaViewModel() {
        Materia = new Materia{
            Nombre="Materia",
            Rubros=["Rubro 1","Rubro 2","Rubro 3"],
            ValorRubros=[0,0,0],
            Calificaciones=[0,0,0]
        };
    } 

    [RelayCommand]
    private void ActualizarDatos() {
        Materia.Nombre = Nombre;
        Materia.Rubros = [Rubro1, Rubro2, Rubro3];
        Materia.ValorRubros =
        [
            double.TryParse(Val1, out var val1) ? val1 : 0,
            double.TryParse(Val2, out var val2) ? val2 : 0,
            double.TryParse(Val3, out var val3) ? val3 : 0
        ];
        OnPropertyChanged(nameof(Materia));
    }

    [RelayCommand]
    private void CalcularCalificacion() {
        Materia.Calificaciones =
        [
            double.TryParse(Cal1, out var cal1) ? cal1 : 0,
            double.TryParse(Cal2, out var cal2) ? cal2 : 0,
            double.TryParse(Cal3, out var cal3) ? cal3 : 0
        ];
        double[] valores = [0,0,0];
        for(int i = 0; i < 3; i++) {
            valores[i] = Materia.ValorRubros[i] > 0 && Materia.ValorRubros[i] <= 100 ? Materia.ValorRubros[i] : 0;
        }
        valores[2] = valores.Sum() == 100 ? valores[2] : (valores.Sum()-valores[2] > 100 ? 0 : 100 - valores.Sum() + valores[2]);
        valores[1] = valores.Sum() == 100 ? valores[1] : 100 - valores.Sum() + valores[1];
        valores[0] = valores.Sum() == 100 ? valores[0] : 100 - valores.Sum() + valores[0];
        Materia.ValorRubros = valores;
        double[] calificaciones = [0,0,0];
        for(int i = 0; i < 3; i++) {
            Materia.Calificaciones[i] = Materia.Calificaciones[i] > 10 ? 10 : Materia.Calificaciones[i];
            Materia.Calificaciones[i] = Materia.Calificaciones[i] < 0 ? 0 : Materia.Calificaciones[i];
            calificaciones[i] = Materia.Calificaciones[i]/10 * Materia.ValorRubros[i];
        }
        Materia.CalificacionTotal = calificaciones.Sum()/10;
        OnPropertyChanged(nameof(Materia));
    }
}
