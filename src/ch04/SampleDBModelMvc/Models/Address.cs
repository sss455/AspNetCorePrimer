using System;
using System.Collections.Generic;

namespace SampleDBModelMvc.Models;

public partial class Address
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;


    // p.102 [Auto] Personオブジェクトのコレクション。
    // PersonテーブルとAddressテーブルが「多：１」の関係になるので、Addressクラスから見ると複数のPersonオブジェクトのデータを扱えるためコレクションになっている。
    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
