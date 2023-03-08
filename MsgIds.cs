
namespace Game
{
    public class MsgIds
    {
       
        public static int FightGainGold = 1005;// 战中获取金币
        public static int GoldChanged = 1006;// 金币改变 广播
        public static int DiamondChanged = 1007;// 钻石改变广播
        public static int RedHeardChanged = 1056;
        public static int GainProp = 1008;// 获取道具
        //public static int GainDelayShow = 1032;// 延迟特效
        public static int UIFresh = 1033;// UI刷新
        public static int FightUiProgress = 1045;//战斗模块 倒计时提醒
        public static int DogeAdTimesChanged = 1046;// UI刷新
        public static int EnergyAdTimesChanged = 1047;// UI刷新
        public static int CreateTaskIcon = 1048;// UI刷新
        public static int TaskIconLight = 1049;// UI刷新
        public static int WpnChipCountChanged = 1053;// UI 刷新 碎片 
        

        public static int CollectingProp = 1025;// 收集中道具
        public static int ExpChanged = 1015;  // 游戏 经验 改变广播
        public static int ActiveProp = 1017;// 战中激活道具 一般都是 跟胜利条件挂钩
        public static int FightUiTopTips = 1018;// 战提示具 提示
        public static int FightUiModeCountDownTips = 1026;//战斗模块 倒计时提醒


        public static int ShowGameTipsByID = 1012;// tips 广播。参数是id
        public static int ShowGameTipsByStr = 1013;// tips 广播 参数是 str
        public static int ShowGameTipsByIDAndParam = 1014;// 广播。参数是id和参数
        public static int AddGameGuideUi = 1034;// 广播 增加引导的手指引
        public static int rmGameGuideUi = 1035;//删除 引导的手指引
        public static int AddGuidePlotUi = 1036;//显示引导提示
        public static int RmGuidePlotUi = 1037;//删除引导提示
        public static int ChangeScrollView4Guide = 1038;//为了引导 调整滑动窗口显示位置

        public static int AddPetName = 1054;//

        public static int TimeCoolDown = 1015;// 游戏倒计时广播
        public static int ShowFightPlotByID = 1023;// tips 广播。参数是id
        public static int ShowFightPlotByStr = 1024;// tips 广播。参数是id
        public static int ShowRewardGetEffect = 1030;// 主界面 获得 奖励效果

        public static int TriggerCollider = 1019;//碰到某个机关

        public static int PointArrowAngle = 1020;//ui 指示箭头的角度

        public static int TaskModeBegin = 1021;// 监听 任务模块开启，
        public static int TaskModeEnd = 1022;// 监听 任务模块关闭，
        public static int TaskModeCountDown = 1050;// 监听 某些任务模块倒计时开始，

        public static int EnergyChanged = 1027;// 能量改变 广播
        public static int MDTChanged = 1028;// 狗牌改变广播

        public static int AfterAD = 1040;// 广告结束广播
        public static int HideGameGuideAD = 1041;// 隐藏游戏引导
        public static int CommonUIIndexChange = 1042;//主界面页签切换 
        public static int ShowAni = 1043;//播放指定动画 
        public static int CommonUIIndexChange4Param = 1044;//主界面页签切换
        public static int CheckGameGuide = 1051;//检查新手引导


        // 多语言变化
        public static int Language = 1055;

        private static int MaxID = 1058;// 不断增大 ，给人看的，添加id 的时候记得 标注一下 

        private static int NewMsgFlag = 3000;//项目新增事件起点

        public static int CurrencyChange = 3030;//货币变化 <CurrencyType>

        public static int PurchaseDataRefresh = 3040;//内购数据刷新
        public static int PurchaseSuccess = 3041;//内购购买成功<isRemedy, productId, rewardList>
        public static int PurchaseFail = 3042;//内购购买失败<status, productId>

        public static int ShopBuySuccess = 3050;//商店购买成功<shopId>

        public static int ServerTimeFixed = 3060;//服务器时间同步<success>
        public static int DateTimeRefresh = 3061;//x日刷新<DateTimeRefreshType>


        public static int TokenSetUnlock = 3201;//设置token锁定状态<isUnlock>

        public static int UIPanelOpen = 4001;//打开UI事件<panelName>
        public static int UIPanelClose = 4002;//关闭UI事件<panelName>
        public static int UIPanelBackToTop = 4003;//UI回到顶层<panelName>
        public static int TopUIBeCovered = 4004;//顶层UI被覆盖<panelName>

     
    }
}
