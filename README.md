# 🛠 win-util-tool

Windows環境で「選択したテキスト」に瞬時に操作できる便利ツールです。
翻訳、説明表示、検索など、ちょっとした調べ物や作業効率化をサポートします。

---

## 🔧 機能一覧

* ✅ 選択文字列の翻訳（DeepL API 使用）
* ✅ Wikipedia説明の取得（Yahoo検索経由）
* ✅ 選択文字列のGoogle検索
* ✅ ホットキー起動（`Ctrl + Shift + C`）
* ✅ OCR機能（`Ctrl + Shift + S`）

---

## ⚠ 開発環境の注意

Visual Studio で開く際は、以下の拡張機能が必要です：

> `Visual Studio Installer Projects`

---

## 🧠 各機能の詳細

### 🔤 選択文字列の翻訳

* 選択中のテキストを **DeepL API** により翻訳。
* 結果はポップアップウィンドウ左下に表示。

⚙ 設定：
システム環境変数 `DEEPL_API_KEY` に APIキーを設定してください。

---

### 📘 Wikipedia説明の取得

* Yahoo検索で `site:wikipedia.org` を指定し検索。
* 検索結果 `.sw-Card__summary` から最大5件の説明文を抽出。
* 結果はポップアップ右下に表示。

🔎 検索URL例：

```
https://search.yahoo.co.jp/search?p=検索文字列 site:wikipedia.org
```

---

### 🔎 Google検索起動

* ポップアップ左下の入力ボックスで `Enter` を押すとGoogle検索を実行。
* ウィンドウは自動的に閉じられます。

---

### 🖼 OCR機能

* ホットキー： `Ctrl + Shift + S`
* 選択範囲をキャプチャし、画像から文字を抽出 → 検索。

⚙ 設定：

1. [`tessdata`](https://github.com/tesseract-ocr/tessdata) または [`tessdata_best`](https://github.com/tesseract-ocr/tessdata_best) をクローン。
2. システム環境変数 `TESSDATA_PATH` にパスを設定。

---

## ⌨ 操作方法まとめ

| 機能     | ホットキー              | 説明               |
| ------ | ------------------ | ---------------- |
| テキスト処理 | `Ctrl + Shift + C` | クリップボードの内容を翻訳・検索 |
| OCR処理  | `Ctrl + Shift + S` | 選択範囲から文字起こし・検索   |

---

## 📝 備考

* .NET Framework 4.7.2 対応
* 依存ライブラリ一覧：[`packages.config`](./win-util-tool/packages.config)
* 初回ビルド前に NuGet パッケージの復元を行ってください：

```bash
nuget restore
```
