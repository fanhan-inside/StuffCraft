﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.50727.42
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace StuffCraft.Properties {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("StuffCraft.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   見出しを入力して下さい。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string BLANK_TITLE_WARNING {
            get {
                return ResourceManager.GetString("BLANK_TITLE_WARNING", resourceCulture);
            }
        }
        
        /// <summary>
        ///   箱＜{0}＞と箱内全ての紙を削除しますか？ に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DELETE_CASE_TEMPLATE {
            get {
                return ResourceManager.GetString("TEMPLATE_DELETE_CASE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   これらの{0}個紙を削除しますか？ に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string DELETE_NOTE_TEMPLATE {
            get {
                return ResourceManager.GetString("TEMPLATE_DELETE_NOTE", resourceCulture);
            }
        }
        
        /// <summary>
        ///   名称には「改行符」など特殊文字は使えません に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string INVALID_NAME {
            get {
                return ResourceManager.GetString("INVALID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   新しい箱 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NEW_CASE_NAME {
            get {
                return ResourceManager.GetString("NEW_CASE_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   新しい紙 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NEW_NOTE_NAME {
            get {
                return ResourceManager.GetString("NEW_NOTE_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   内容は「空白」出来ません。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string String1 {
            get {
                return ResourceManager.GetString("String1", resourceCulture);
            }
        }
    }
}