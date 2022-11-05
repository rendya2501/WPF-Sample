# MultiSelectComboBox開発について

---

## 概要

マルチセレクトコンボボックスの開発あれこれ。  
実務で必要になったが、自分で作るとなると難しかった。
開発によって得られた知見をまとめる。  

実務ではxceedソフトのCheckComboBoxを継承して、少し改造して実装することができた。  
しかし、不満点もいくつかあったので、ゆくゆくは自作したい。  

---

## 自作マルチセレクトコンボボックス

・ディスプレイメンバー  
・セレクトバリュー  
・中間状態の見た目  
・そもそもの解析  

[【C#】ListBoxで項目を追加、取得する方法(CheckedListBoxも解説)](https://www.sejuku.net/blog/57045)  
[CheckBox Styles and Templates 公式ソース](https://docs.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/cc278078(v=vs.95))  
