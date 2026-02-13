/* p.XX [Auto] スキャフォールディングで自動生成 */
using System.ComponentModel.DataAnnotations;

namespace SampleMvc.Models;

public partial class Person
{
    //-------------------------------------------------------------------
    // Id
    //-------------------------------------------------------------------
    public int Id { get; set; }


    //-------------------------------------------------------------------
    // 名前（Name）
    //-------------------------------------------------------------------
    // p.57 [Add] Display属性を追加
    [Display(Name="名前")]
    // p.75 [Add] MaxLength属性を追加
    [MaxLength(10, ErrorMessage="名前は10文字以内でお願いします")]
    public string Name { get; set; } = null!;


    //-------------------------------------------------------------------
    // 年齢（Age）
    //-------------------------------------------------------------------
    // p.57 [Add] Display属性を追加
    [Display(Name = "年齢")]
    // p.58 [Add] DisplayFormat属性を追加
    [DisplayFormat(DataFormatString="{0} 歳")]
    // p.75 [Add] Range属性を追加
    [Range(18, 100, ErrorMessage="年齢は18歳から100歳までです")]
    public int Age { get; set; }
}
