using System;
using System.Collections.Generic;
using System.Text;

namespace StuffCraft
{
    class clsMessage
    {
        public static string NEW_CASE_NAME = @"新しい箱";
        public static string NEW_NOTE_NAME = @"新しい紙";
        public static string INVALID_NAME = @"名称には「改行符」など特殊文字は使えません";
        public static string BLANK_TITLE_WARNING = @"見出しを入力して下さい。";
        public static string BLANK_CONTENT_WARNING = @"内容は「空白」出来ません。";
        public static string EXPORT_OK_MESSAGE = @"出力完了しました。";
        public static string TEMPLATE_DELETE_CASE = @"箱＜{0}＞と箱内全ての紙を削除しますか？";
        public static string TEMPLATE_DELETE_NOTE = @"これらの{0}個紙を削除しますか？";
        public static string TEMPLATE_CASE_TOOLTIP = @"紙：{0}件";
    }
}
