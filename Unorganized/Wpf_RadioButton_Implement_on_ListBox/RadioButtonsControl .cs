using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_RadioButton_on_ListBox
{
    /// <summary>
    /// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
    ///
    /// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Wpf_RadioButton_on_ListBox"
    ///
    ///
    /// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
    /// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
    /// 追加します:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Wpf_RadioButton_on_ListBox;assembly=Wpf_RadioButton_on_ListBox"
    ///
    /// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
    /// リビルドして、コンパイル エラーを防ぐ必要があります:
    ///
    ///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
    ///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
    ///
    ///
    /// 手順 2)
    /// コントロールを XAML ファイルで使用します。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class RadioButtonsControl : ListBox
    {
        /// <summary>
        /// RadioButtonControl.Columns 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(RadioButtonsControl), new FrameworkPropertyMetadata(0));

        /// <summary>
        /// RadioButtonControl.Rows 依存関係プロパティを識別します。
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(RadioButtonsControl), new FrameworkPropertyMetadata(0));

        /// <summary>
        /// コントロール内にラジオボタンを並べる行数を取得または設定します。これは、依存関係プロパティです。
        /// </summary>
        /// <value>コントロール内の行の数。既定値は 0 です。</value>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// コントロール内にラジオボタンを並べる列数を取得または設定します。これは、依存関係プロパティです。
        /// </summary>
        /// <value>コントロール内の列の数。既定値は 0 です。</value>
        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static RadioButtonsControl()
        {
            // プロパティのデフォルト値を上書きする
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioButtonsControl), new FrameworkPropertyMetadata(typeof(RadioButtonsControl)));
            BackgroundProperty.OverrideMetadata(typeof(RadioButtonsControl), new FrameworkPropertyMetadata(Brushes.Transparent));
            BorderBrushProperty.OverrideMetadata(typeof(RadioButtonsControl), new FrameworkPropertyMetadata(Brushes.Transparent));
        }
    }
}
