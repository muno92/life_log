namespace SpeedTest.Resource;

// 現在だとBandwidthがintの上限（約2.4Gbps）を超えるのは考えにくいが、将来的に速度が上がるかもしれないのでlongにしておく
public record SpeedDetail(long Bandwidth, int Bytes, int Elapsed);
