/* p.44 [Auto] スキャフォールディングで自動生成 */
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleMvc.Models;

namespace SampleMvc.Controllers
{
    public class PeopleController : Controller
    {
        // DBコンテキスト
        private readonly MvcdbContext _context;


        // コンストラクター
        public PeopleController(MvcdbContext context) // 依存性の注入
        {
            _context = context;
        }


        #region 一覧画面
        // p.51 一覧画面表示
        // GET: People
        public async Task<IActionResult> Index()
        {
            //IEnumerable<Person> people = await _context.People.ToListAsync();
            //return View(people);

            //return View(await _context.People.ToListAsync());

            //return View(await _context.People.Take(100).ToListAsync());

            return _context.People != null ?
                   View(await _context.People.ToListAsync()) :
                   Problem("Entity set 'MvcdbContext.People' is null.");

            //-----------------------------------------------------------------------------------------------------
            // Controllerクラスの各メソッドは必ずIActionResultインターフェイスを返している。
            //
            //【 ActionResultクラスを継承しているクラス】
            // ・Viewメソッド：「ViewResultクラス」を生成する。これはActionResultクラスというIActionResultインターフェイスを実装したクラスを継承している。
            // ・ページを直接ジャンプするためのRedirectResultクラス
            // ・バイナリデータを返すための「FileResultクラス」
            // ・JSONデータを返すためのJsonResultクラス
            // など
            //-----------------------------------------------------------------------------------------------------
        }

        //// 同期処理のIndexメソッド
        //public IActionResult Index()
        //{
        //    return View(_context.People.ToList());
        //}
        #endregion


        #region 詳細画面
        // 詳細画面表示
        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            if (id == null || _context.People == null)
                {
                return NotFound(); // HTTP 404 エラー
            }

            var person = await _context.People
                                .FirstOrDefaultAsync(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        #endregion


        #region 新規登録画面
        // p.68 新規登録画面表示
        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // p.69 登録ボタン押下処理
        //   POST: People/Create
        //   To protect from overposting attacks, enable the specific properties you want to bind to.
        //   For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //   (訳)
        //   過剰なPOST攻撃から保護するため、バインドしたい特定のプロパティを有効にしてください。
        //   詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        //-----------------------------------------------------------------------------------------------
        [HttpPost] // HttpPost属性：POST形式でデータを受け取ることを示す
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( // Createメソッドをオーバーロード
                                            [Bind("Id,Name,Age")] Person person) // 画面の入力値が引数のpersonオブジェクトにバインドされる
        {
            // Modelクラスの検証属性のチェック結果（入力エラーの場合はfalseを返す）
            if (ModelState.IsValid)
            {
                // p.71 [Add] 検証属性チェック以外の何らかの入力値チェックはここに追加
                if( person.Age < 20 )
                {
                    // ModelState.AddModelErrorメソッドで、エラーメッセージを設定
                    ModelState.AddModelError("Age", "二十歳未満です。");
                    return View(person);
                }
                
                _context.Add(person);                   // 1.コンテキストに入力データ(person)をAddメソッドで追加
                await _context.SaveChangesAsync();      // 2.SaveChangesAsyncメソッドでデータベースに反映
                return RedirectToAction(nameof(Index)); // 3.DB登録後、Indexページにリダイレクト
            }

            // 入力エラーの場合、受け取ったデータをそのまま返してViewメソッドを呼び出す
            return View(person);
        }
        #endregion


        #region 編集画面
        // p.77 編集画面表示
        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            /* Detailsメソッドと同じ作り */

            // if (id == null)
            if (id == null || _context.People == null)
                {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // p.78 編集ボタン押下時
        //   POST: People/Edit/5
        //   To protect from overposting attacks, enable the specific properties you want to bind to.
        //   For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //   (訳)
        //   過剰なPOST攻撃から保護するため、バインドしたい特定のプロパティを有効にしてください。
        //   詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        //-----------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
        #endregion


        #region 削除画面
        // 削除画面表示
        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // 削除ボタン押下時
        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
        #endregion
    }
}
