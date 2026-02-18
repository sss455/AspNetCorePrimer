using System.ComponentModel.DataAnnotations;

namespace SampleCFModelMvc.Models;

public class Person
{
    public int Id { get; set; }

    [Display(Name="氏名")]
    public string Name { get; set; } = null!;

    [Display(Name="年齢")]
    public int Age { get; set; }


    // Addressへ外部リンク
    public int AddressId { get; set; }
    public Address Address { get; set; } = null!;
}
