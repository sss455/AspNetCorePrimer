namespace SampleDBModelMvc.Models;

public partial class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }


    // p.102 [Auto] 外部キー
    public int? AddressId { get; set; }
    // p.102 [Auto] Addressオブジェクトを参照できるプロパティ
    public virtual Address? Address { get; set; }
}
