using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TDMPW_411_3P_EX.Models;

namespace TDMPW_411_3P_EX.ViewModels;

public partial class SemestreViewModel : ObservableObject
{
    [ObservableProperty]
    private Semestre _semestre;

    [ObservableProperty]
    private string _nombre;

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

    public SemestreViewModel() {
        Semestre = new Semestre {
            Nombre = "Materia",
            ValorParciales = [0,0,0],
            Calificaciones = [0,0,0]
        };
    } 

    [RelayCommand]
    private void ActualizarDatos() {
        Semestre.Nombre = Nombre;
        Semestre.ValorParciales =
        [
            double.TryParse(Val1, out var val1) ? val1 : 0,
            double.TryParse(Val2, out var val2) ? val2 : 0,
            double.TryParse(Val3, out var val3) ? val3 : 0
        ];
        OnPropertyChanged(nameof(Semestre));
    }
}
