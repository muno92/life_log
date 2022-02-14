[![QA](https://github.com/muno92/life_log/actions/workflows/qa.yml/badge.svg)](https://github.com/muno92/life_log/actions/workflows/qa.yml)

日々の生活におけるメトリクスを収集するプログラム

- [NatureRemoMonitor](NatureRemoMonitor)
  - Nature RemoのAPIから温度・湿度・照度を収集してスプレッドシートに記録
  - GitHub Actionsで定期実行
- [SpeedTest](SpeedTest)  
  - [speedtest cli](https://www.speedtest.net/ja/apps/cli) で回線速度を計測し、スプレッドシートに記録
  - ローカルPC上でcronを用いて定期実行
