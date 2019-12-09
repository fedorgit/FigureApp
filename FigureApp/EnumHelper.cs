using System.ComponentModel.DataAnnotations;

namespace FigureApp
{
    public enum PointStatusType
    {
        [Display(Name = "Корректно")]
        Correct = 1,

        [Display(Name = "Нет файла")]
        NoFile = 2,

        [Display(Name = "Ошибка количества точек")]
        ErrorAmountPoint = 3,
        
        [Display(Name = "Ошибка формата числового значения")]
        ErrorValueFormat = 4,
        
        [Display(Name = "Ошибка допустимого диапазона значения")]
        ErrorValidRange = 5
    }
}
