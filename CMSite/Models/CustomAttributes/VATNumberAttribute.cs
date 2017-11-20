using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMSite.Models.CustomAttributes
{
    public class VATNumberAttribute : DataTypeAttribute
    {
        public VATNumberAttribute() : base(DataType.Text)
        {
            ErrorMessage = "非有效之統一編號！";
        }

        public override bool IsValid(object value)
        {
            #region 傳入值為空時，不進行驗證
            if (value == null)
            {
                return true;
            }
            #endregion

            string vat = value as string;

            #region (條件一)8碼長度驗證
            if (vat.Length != 8)
            {
                ErrorMessage = "統一編號為 8 碼，請確認！";
                return false;
            }
            #endregion
            #region (條件二)邏輯驗證
            int vatSum = 0;
            int tempCompute = 0;
            //處理第1~6碼
            for (int i = 1; i <= vat.Length; i++)
            {
                tempCompute = Convert.ToInt32(vat.Substring(i - 1, 1));
                if (i == 7)
                {
                    tempCompute = tempCompute * 4;
                }
                else
                {
                    if (i % 2 == 0 && i != 8)
                    {
                        tempCompute = tempCompute * 2;
                    }
                }
                vatSum += (tempCompute / 10) + (tempCompute % 10);
            }

            if (vatSum % 10 == 0
                || (vat[6] == '7' && (vatSum - 9) % 10 == 0))
            {
                return true;
            }
            else
            {
                ErrorMessage = "非有效之統一編號！";
                return false;
            }

            #endregion
        }
    }
}