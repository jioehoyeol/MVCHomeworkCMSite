using CMSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSite.DataAccess
{
    public class CustomerBankDAO : BaseDAO
    {
        /// <summary>
        /// 取得客戶銀行資訊
        /// </summary>
        /// <returns></returns>
        public IEnumerable<客戶銀行資訊> QueryData(string keyword)
        {
            var result = from b in db.客戶銀行資訊
                         where b.IsDelete == false
                         select b;

            if (!string.IsNullOrEmpty(keyword))
            {
                result = result.Where(bank => bank.帳戶名稱.Contains(keyword)
                                           || bank.銀行名稱.Contains(keyword)
                                           || bank.客戶資料.客戶名稱.Contains(keyword));

            }

            return result;
        }

    }
}