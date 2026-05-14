using System;
using System.Drawing;
using System.Windows.Forms;

namespace WF2WWinFormDemo
{
    public partial class CalculatorForm : Form
    {
        // 计算相关变量
        private double _firstNumber = 0;
        private double _secondNumber = 0;
        private string _operation = string.Empty;
        private bool _isNewInput = true;

        // 布局相关：按钮的行列数、间距（间距占窗体比例，避免间距过大/过小）
        private readonly int _columnCount = 4; // 按钮列数
        private readonly int _rowCount = 5;    // 按钮行数
        private readonly int _fixedPadding = 8; // 固定间距（像素），保证控件之间有间隙但不影响填充

        public CalculatorForm()
        {
            InitializeComponent();

            // 初始化计算界面
            txtDisplay.Text = "0";

            // 设置窗体初始大小和最小大小（防止缩放过小导致控件重叠）
            this.Size = new Size(400, 500);
            this.MinimumSize = new Size(280, 350);

            // 绑定Resize事件（窗体大小改变时触发布局重算）
            this.Resize += CalculatorForm_Resize;
            // 首次加载强制计算布局
            this.Load += (s, e) => UpdateControlLayout();
        }
        #region 布局计算核心方法（修正版：填充式布局）
        private void CalculatorForm_Resize(object sender, EventArgs e)
        {
            UpdateControlLayout();
        }
        /// <summary>
        /// 动态更新所有控件的位置和大小（完全填充窗体，无空白）
        /// </summary>
        private void UpdateControlLayout()
        {
            // 获取窗体客户端区域（排除边框）
            Rectangle clientRect = this.ClientRectangle;

            // 1. 处理显示屏（占顶部整行，高度占客户端区域的1/6，剩余部分给按钮）
            int displayHeight = clientRect.Height / 6;
            // 显示屏位置：左、上、右间距为fixedPadding，高度为displayHeight
            txtDisplay.Location = new Point(_fixedPadding, _fixedPadding);
            var newSize = new Size(clientRect.Width - 2 * _fixedPadding, displayHeight - 2 * _fixedPadding);
            //if(newSize.Width < 150)
            //{
            //    var s = 1;
            //}
            txtDisplay.Size = newSize;
            // 显示屏字体自适应（基于显示屏高度）
            txtDisplay.Font = new Font("Microsoft YaHei", displayHeight / 3, FontStyle.Regular);

            // 2. 计算按钮区域的可用空间（显示屏下方的全部区域）
            Rectangle buttonArea = new Rectangle(
                _fixedPadding,
                displayHeight + _fixedPadding,
                clientRect.Width - 2 * _fixedPadding,
                clientRect.Height - displayHeight - 2 * _fixedPadding
            );

            // 3. 计算每列的宽度和每行的高度（均分按钮区域，扣除间距）
            // 列宽：(按钮区域宽度 - (列数+1)*固定间距) / 列数 （列数+1：左右边距+列之间的间距）
            int columnWidth = (buttonArea.Width - (_columnCount + 1) * _fixedPadding) / _columnCount;
            // 行高：(按钮区域高度 - (行数+1)*固定间距) / 行数
            int rowHeight = (buttonArea.Height - (_rowCount + 1) * _fixedPadding) / _rowCount;

            // 4. 按行列更新按钮位置和大小（完全填充按钮区域）
            // 第一行：7,8,9,÷
            SetButtonPosition(btn7, 0, 0, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btn8, 1, 0, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btn9, 2, 0, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btnDivide, 3, 0, columnWidth, rowHeight, buttonArea);

            // 第二行：4,5,6,×
            SetButtonPosition(btn4, 0, 1, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btn5, 1, 1, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btn6, 2, 1, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btnMultiply, 3, 1, columnWidth, rowHeight, buttonArea);

            // 第三行：1,2,3,-
            SetButtonPosition(btn1, 0, 2, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btn2, 1, 2, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btn3, 2, 2, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btnMinus, 3, 2, columnWidth, rowHeight, buttonArea);

            // 第四行：0（占两列）,. ,=
            // 0按钮：列0-1，宽度=2*列宽+固定间距（填充两列空间）
            btn0.Size = new Size(2 * columnWidth + _fixedPadding, rowHeight);
            SetButtonPosition(btn0, 0, 3, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btnDot, 2, 3, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btnEquals, 3, 3, columnWidth, rowHeight, buttonArea);

            // 第五行：C,←,±,+
            SetButtonPosition(btnClear, 0, 4, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btnBackspace, 1, 4, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btnNegate, 2, 4, columnWidth, rowHeight, buttonArea);
            SetButtonPosition(btnPlus, 3, 4, columnWidth, rowHeight, buttonArea);

            // 按钮字体自适应（基于行高）
            SetButtonFont(new[] { btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btnDot, btnEquals, btnClear, btnBackspace, btnNegate, btnPlus, btnMinus, btnMultiply, btnDivide }, rowHeight);
        }

        /// <summary>
        /// 设置单个按钮的位置（基于行列索引，填充按钮区域）
        /// </summary>
        /// <param name="btn">按钮控件</param>
        /// <param name="col">列索引（0-3）</param>
        /// <param name="row">行索引（0-4）</param>
        /// <param name="colWidth">单列宽度</param>
        /// <param name="rowHeight">单行高度</param>
        /// <param name="buttonArea">按钮区域的矩形范围</param>
        private void SetButtonPosition(Button btn, int col, int row, int colWidth, int rowHeight, Rectangle buttonArea)
        {
            // 计算X坐标：按钮区域左边界 + 间距 + 列索引*(列宽+间距)
            int x = buttonArea.X + _fixedPadding + col * (colWidth + _fixedPadding);
            // 计算Y坐标：按钮区域上边界 + 间距 + 行索引*(行高+间距)
            int y = buttonArea.Y + _fixedPadding + row * (rowHeight + _fixedPadding);
            btn.Location = new Point(x, y);
            // 除了0按钮，其他按钮设置为单列宽度和单行高度
            if (btn != btn0)
            {
                btn.Size = new Size(colWidth, rowHeight);
            }
        }

        /// <summary>
        /// 设置按钮字体大小（基于行高，保证字体适配按钮大小）
        /// </summary>
        /// <param name="buttons">按钮数组</param>
        /// <param name="rowHeight">按钮行高</param>
        private void SetButtonFont(Button[] buttons, int rowHeight)
        {
            float fontSize = rowHeight / 2; // 字体高度为按钮高度的一半，最佳显示效果
            fontSize = Math.Max(8, Math.Min(24, fontSize)); // 限制字体大小范围（8-24号）
            foreach (var btn in buttons)
            {
                btn.Font = new Font("Microsoft YaHei", fontSize, FontStyle.Regular);
            }
        }
        #endregion

        #region 计算器业务逻辑（保持不变）
        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            if (_isNewInput)
            {
                txtDisplay.Text = string.Empty;
                _isNewInput = false;
            }

            if (btn.Text == "." && txtDisplay.Text.Contains("."))
            {
                return;
            }

            txtDisplay.Text += btn.Text;
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            if (double.TryParse(txtDisplay.Text, out _firstNumber))
            {
                _operation = btn.Text;
                _isNewInput = true;
            }
        }

        private void BtnEquals_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtDisplay.Text, out _secondNumber))
            {
                txtDisplay.Text = "错误";
                _isNewInput = true;
                return;
            }

            double result = 0;
            try
            {
                switch (_operation)
                {
                    case "+":
                        result = _firstNumber + _secondNumber;
                        break;
                    case "-":
                        result = _firstNumber - _secondNumber;
                        break;
                    case "×":
                        result = _firstNumber * _secondNumber;
                        break;
                    case "÷":
                        if (_secondNumber == 0)
                        {
                            txtDisplay.Text = "除数不能为0";
                            _isNewInput = true;
                            return;
                        }
                        result = _firstNumber / _secondNumber;
                        break;
                    default:
                        result = _secondNumber;
                        break;
                }

                txtDisplay.Text = result % 1 == 0 ? result.ToString("0") : result.ToString();
                _isNewInput = true;
            }
            catch
            {
                txtDisplay.Text = "错误";
                _isNewInput = true;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            _firstNumber = 0;
            _secondNumber = 0;
            _operation = string.Empty;
            _isNewInput = true;
        }

        private void BtnBackspace_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length > 1)
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
            else
            {
                txtDisplay.Text = "0";
                _isNewInput = true;
            }
        }

        private void BtnNegate_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text != "0")
            {
                txtDisplay.Text = txtDisplay.Text.StartsWith("-")
                    ? txtDisplay.Text.Substring(1)
                    : $"-{txtDisplay.Text}";
            }
        }
        #endregion
    }
}