# 【PORTFOLIO】
<img width = "1000" src="Images/Portfolio_Image_1_2.png">

## 【自己紹介】
閲覧いただき、ありがとうございます。藤嶌 諒です。

私はUnityを使用してチームでゲーム制作をしています。

私がゲーム作りを行う目的は、「自身とユーザーを満足させるゲームを作る」ことです。

大切にしている言葉は「Trust me」と「ゲーム作りを楽しめ」です。

私の趣味はゲームです。最近プレイしたゲームは「Coffee Talk」です。ゲームで登場する「生姜コーヒー」を実際に作ったのですが、とても美味しかったです。生姜は少し多めくらいがオススメです。

私はVRに興味があり、最近はフェイシャルトラッカーを使用して「笑ってはいけないVR」というゲームを作ったら面白そうではないか？と妄想を膨れませました。

## 【Real Glitter】 
私はReal Glitterというチーム名義でゲームを制作しています。チームのコアメンバーは私と兄の2人です。
* 私たちは互いにゲームデザインを担い、ゲームを面白くするために基本的に週1回の意見交換を設け、活発なイテレーションを行ってきました。
* 3Dモデルや2Dアート等の二人では補えない要素は、兄の知人に協力していただくことによってゲームをさらに面白いものにすることが出来ました。
* チーム制作を通して、技術面以外の部分でも多くのことを学びました。


## 【VR Game Project】

<img width = "800" src="Images/BAB_Title_Picture.png">

### 情報
* タイトル : BAB
* [販売サイト(Steam)](https://store.steampowered.com/app/2919580/BAB/)
* ジャンル : VR間違い探しアクションホラー
* 人数 : シングルプレイ
* プラットフォーム : SteamVR
* プレイスタイル : エリア固定, 着席 
* 制作期間 : 2022年7月 ~ 2024年5月
* 制作環境 : Windows11, Unity
* 担当 : プログラミング全般, ゲームデザイン 

### ハイハイ
<img width = "600" src="Images/Portfolio_Haihai.gif">

BABはハイハイで移動する。

#### ハイハイとは
VRゲームプロジェクトにはいくつかの開発課題を設定しており、これらを達成すべく試行錯誤を繰り返してきました。
ハイハイは課題の1つである「酔わない」という課題を達成すべく作成されました。

#### ハイハイの誕生
ハイハイは兄が考えつきました。兄は「手を使った動き」に着目しハイハイにたどり着きました。
私は「酔わない」システムを制作するために、「酔わないシステム」とは何かについて考えていました。

私がたどり着いた酔わないシステムの考えは、以下の条件を全て満たすものです。
* プレイヤーの想像とゲームでの結果が同一である。
* 体を動かす。

(例 : ハイハイ)
* ゲーム内の地面に仮想の手を着く 
* 手を交互に地面に着ける (体を動かす)
* プレイヤーはハイハイを想像できる (想像)
* ゲーム内でハイハイするように進行する (結果と同一)

私の考えは兄のアイデアを補強するような形で活かされました。
また、この考えはゲームシステムを制作する前の段階で、酔うか酔わないかを判断できる材料の1つとなりました。

#### ハイハイシステムの歴史
**"ハイハイシステムの歴史 1 : まずは行動"**
* ハイハイを作る最初のステップは似たシステムを探しコピペし触ることでした。
* 深く考える前に行動した理由は、私にハイハイを0から作る能力がないと判断したためです。また、似たシステムを扱うことによってインスピレーションを得られると考えました。

**"ハイハイシステムの歴史 2 : 気づきと方針"**
* コピペしたシステムを触ることによって、私が何を求めているかを明確にさせることが出来ました。そして、私の理解力では作成できない部分も分かりました。
* 求めているシステムを実現させるために、システムの新規実装とコピペシステムの一部を維持することを決定しました。
<img width = "600" src="Images/haihai_koretsukuritai_1.png">

**"ハイハイシステムの歴史 3 : 立ちふさがる困難"**
* 求めているシステムを作成する上で、最も困難であったのが「両手を使って移動する」ことでした。ハイハイは両手を使って動くのが一般的であるためです。
* 困難であった理由は、当時の私の知識には「1つの入力で対象を動かす」しかなかったためです。
* この問題を解決するために「ハイハイ」という動きをよく観察しました。気づいたことは、「最初に着いた手と後に着いた手が異なる場合、進むことができる」ということでした。
* 気づきを実現するために、私の知識を全て展開し使えるものを探しました。
* 最初に見つけたのは「List」でした。Listを使用することによって前後の比較を行うことができるためです。この「前後の比較」という考えに基づき、Listは後にシンプルにするために「string」に変わります。
* こうして私は「両手で移動するハイハイ」を作ることができました。
<img width = "800" src="Images/Haihai_letslist_0.png">

**"ハイハイシステムの歴史 4 : 現実的vs非現実的"**
* この問題は「後ろハイハイ」の作成中に発生しました。
* プレイヤーが前に進みたいのか、後ろに進みたいのかを明確にさせるために、手を着いたときの両手の位置に着目しました。
<img width = "800" src="Images/haihai_tenoiti_1.png">

* どちらのシステムも試した後、私たちは「操作性の良さ」を優先させました。ハイハイは一般的にゲームにおいて親しみのない操作であるため、プレイヤーのやりやすさを重視すべきと判断したためです。
* ハイハイの操作がやりやすいものになることによって、ハイハイをストレスなく楽しめるものに作りあげることが出来ました。

**"ハイハイシステムの歴史 : まとめ"**
* "ハイハイシステムの歴史"ではハイハイのシステムをいくつかピックアップして紹介させていただきました。
* ハイハイは、問題を解決しつつ段階的により良くすることによって、プレイヤーが違和感を感じず、操作性の良いアクションにすることができました。
* BABは体験版もリリースしておりますので、ぜひハイハイに触れてみていただきたいです。

#### ハイハイに関連するスクリプト
none

#### リファクタリングを行ったスクリプト
none

### インタラクション
<img width = "600" src="Images/Portfolio_Interaction.gif">

BABは「触れる」と「近くで見る」をすることでインタラクトできる。

#### 協調性のあるアクション
「触れる」と「近くで見る」はインタラクションのアクション部分になります。アクション単体で見るとユニークさやインパクトにも欠けるかもしれませんが、
この2つのアクションは他の要素を尊重しており、ゲームに馴染むように設計されています。
<img width = "600" src="Images/interaction_2.png">

**"ボタン入力を使用しないことにより、没入感を損なわさせない"**
* ゲーム内で手に機械や装置を握っている場合はコントローラーのボタン入力を押すことに違和感はありません。
* しかし、手に何も握っていない場合にボタン入力を行うことは違和感があり、途端に「ゲームチック」に感じてしまいます。これはVR特有の「現実に近い」という利点を活かせなくなってしまいます。
* 以上の理由から、2つのアクションはボタン入力を避け、没入感を維持する工夫がされています。

**"ハイハイを活かし補う"**
* 「触れる」の利点は、ハイハイしていた状態から手を伸ばすだけで行えることです。移動とアクションがスムーズに行えるため、プレイヤーにストレスを与えず、ゲーム体験に集中してもらうことができます。
* 「近くで見る」は、手が届かない所に対してもインタラクションを可能にします。このアクションによりゲーム内の空間を最大限有効活用でき、プレイヤーにより自由さを感じさせることができます。

**"ゲームシステムに合うアクション"**
* 人は気になったものを「手に持つ」「注視する」ような行動を取ります。この行動は間違い探しにも適していると考えました。
* ゲームに導入する際に「手に持つ」「注視する」というアクションを、よりシンプルな「触れる」「近くで見る」に置き換えました。
* シンプルなものにしたは理由は、「テンポの良いゲームにしたい」「リソースを削減したい」という意図があったためです。テンポの良いゲームはプレイヤーを飽きさせないために重要であり、リソース削減は開発速度を高めるためです。


**「触れる」と「近くで見る」**
このように、2つのアクションは他の要素と組み合わせることで、ゲームに馴染む自然なアクションに仕上がっています。









## 【License】

### Code
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

### Media Files
All images, GIFs, and videos included in this repository are copyrighted by their respective owners and are provided here solely for educational and illustrative purposes. They may not be copied, modified, or redistributed without explicit permission from the copyright holder.
