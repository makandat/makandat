/* express + ejs のサンプル */
'use strict';

// モジュールをロード。
var express = require("express");
var fs = require("fs");
var mysql = require("mysql");

// 変数 app に express オブジェクトを割り当てる。
var app = express();
// 設定情報 (内部で使用する)
var config = {};
// バージョン情報 (内部で使用する)
const Version = "1.0";

// テンプレートエンジンとして ejs をロードする。
app.engine('ejs', require('ejs').__express);
// テンプレートエンジンが ejs であることを指定 (render()で拡張子の指定が不要になる)
app.set("view engine", "ejs");
// 静的ファイルの場所を指定。
app.use(express.static('static'));
// フォームパラメータを JSON 形式にするミドルウェアを使用。
app.use(express.json());
// フォームパラメータの HTTP 形式から JavaScript 形式にするミドルウェアを使用。
app.use(express.urlencoded({ extended: true }));



/*  ヘルプ関数  */
//  INIファイル config.ini を読み込む。
function readini() {
  let conf = fs.readFileSync("config.ini", "utf-8");
  let result = {};
  let lines = conf.split("\n");
  for (let l of lines) {
    let p = l.split("=");
    if (p.length == 2) {
      result[p[0]] = p[1].trim();
    }
  }
  return result;
}


/*  ルートハンドラの定義  */
// root ('/'):  index.html へリダイレクト
app.get("/", (req, res)=>{
  console.log("route /");
  res.redirect("/index.html");
});

// /about : about.ejs をレンダリング
app.get("/about", (req, res)=>{
  res.render("about.ejs", {"version":Version});
});


// MySQL test (SELECT)  route変数 viewname を使う例
app.get("/mysql/:viewname", (req, res)=>{
  let viewname = req.params["viewname"];
  let sql = "SELECT * FROM " + viewname;
  console.log("/mysql " + sql);
  let conn = mysql.createConnection({
    host: config["host"],
    user: config["uid"],
    password: config["pwd"],
    database: config["db"]
  });
  conn.connect();
  conn.query(sql, (error, results, fields)=>{
    res.render("mysql.ejs", {"results":results});
  });
});

// MySQL test (INSERT): ins_asset.ejs をレンダリング
app.get("/ins_asset", (req, res)=>{
  console.log("GET /ins_asset");
  res.render("ins_asset.ejs", {"message":""});
});

// MySQL test (INSERT): POST されたフォームを使う例
app.post("/ins_asset", (req, res, next)=>{
  console.log("POST /ins_asset");
  let date = req.body["date"];
  let asset = req.body["asset"];
  let profit = req.body["profit"];
  let sql = `INSERT INTO YJFX_Asset VALUES(NULL, '${date}', ${asset}, ${profit}, 'TEST')`;
  console.log(sql);
  let conn = mysql.createConnection({
    host: config["host"],
    user: config["uid"],
    password: config["pwd"],
    database: config["db"]
  });
  conn.connect();
  conn.query(sql, (error, response)=>{
    let message = "";
    if (error)
      message = "エラー：" + error.message;
    else
      message = "正常終了 " + date;
    res.render("ins_asset.ejs", {"message":message});
  });
});

// ajax_echo
app.get("/ajax/echo", (req, res)=>{
  let msg = req.query["echo"];
  console.log("/ajax/echo echo=" + msg);
  res.set("Content-Type", "text/plain");
  res.status(200).send(msg);
});

// ajax_match
app.get("/ajax/match", (req, res)=>{
  let text = req.query["text"];
  let regex = req.query["regex"];
  console.log("/ajax/match " + regex);
  let re = new RegExp(regex, "g");
  let found = re.exec(text);
  let result = [];
  while (found != null) {
    //console.log(found[0]);
    result.push(found[0]);
    found = re.exec(text);
  }
  let json = JSON.stringify(result);
  res.set("Content-Type", "application/json");
  res.status(200).send(json);
});




// Start up.
config = readini();
console.info("Start sever port=3000 ...");
app.listen(3000);
