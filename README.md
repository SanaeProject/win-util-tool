# win-util-tool

Windows環境で「選択したテキスト」に対して瞬時に操作できる便利ツールです。  
翻訳・説明表示・検索など、ちょっとした調べ物や作業効率化をサポートします。

---

## 主な機能

- [x] **選択文字列の翻訳**（DeepL API 使用）
- [x] **Wikipedia説明の取得**（Yahoo検索経由）
- [x] **選択文字列のGoogle検索**
- [x] **ホットキー起動：Ctrl + Shift + C**
- [x] **OCR機能：Ctrl + Shift + S**

---

## 詳細機能

### 選択文字列の翻訳

- 選択中のテキストを **DeepL翻訳API** を使って翻訳。
- 結果はポップアップウィンドウ左下のテキストボックスに表示。

> [!NOTE]
> 利用には DeepL API のキーが必要です。  
> システム環境変数に `DEEPL_API_KEY` を設定してください。

---

### Wikipedia説明の表示

- 選択文字列を Yahoo検索（`site:wikipedia.org`）で検索。
- 検索結果の `.sw-Card__summary` を抽出し、**最大5件**の説明文を取得。
- ポップアップ右下のテキストボックスに表示。

検索URL形式：  
`https://search.yahoo.co.jp/search?p=検索文字列 site:wikipedia.org`

---

### 選択文字列のGoogle検索

- ポップアップ左下のテキストボックスで `Enter` を押すと、Google検索ページが起動。
- ポップアップウィンドウは自動的に閉じます。

---

### OCR機能

- ホットキー： `Ctrl + Shift + S`
  -> 選択した範囲を文字起こしし検索します。
> [!NOTE]
> 利用には ['tessdata'](https://github.com/tesseract-ocr/tessdata) 若しくは ['tessdata_best'](https://github.com/tesseract-ocr/tessdata_best) をクローンしてください。
> システム環境変数に `TESSDATA_PATH`　にクローンしたパスを設定してください。

---

## 操作方法

- ホットキー： `Ctrl + Shift + C`  
  -> クリップボードの情報が検索されます。
  
---

## 備考

- このツールは .NET Framework 4.7.2 対応  
- 使用ライブラリ一覧は [`packages.config`](./win-util-tool/packages.config) を参照してください
- 初回実行時は `NuGet パッケージの復元` を行ってください

```bash
nuget restore
