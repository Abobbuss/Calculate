using System;
using System.Windows.Forms;
using Calculate.Expression;

namespace Calculate.UI;

public partial class CalculatorForm : Form
{
    private ExpressionEvaluator _evaluator;

    public CalculatorForm()
    {
        InitializeComponent();
        _evaluator = new ExpressionEvaluator();
        display.ReadOnly = true;
    }

    private void OnNumberButtonClick(object sender, EventArgs e)
    {
        if (sender is Button button)
            display.Text += button.Text;
    }

    private void OnOperatorButtonClick(object sender, EventArgs e)
    {
        if (sender is Button button)
            display.Text += $"{button.Text}";
    }
    
    private void OnEqualButtonClick(object sender, EventArgs e)
    {
        try
        {
            double result = _evaluator.Evaluate(display.Text);
            display.Text = result.ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            display.Text = "";
        }
    }

    private void OnClearButtonClick(object sender, EventArgs e)
    {
        display.Text = "";
    }

    private void OnBackspaceButtonClick(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(display.Text))
            display.Text = display.Text.Substring(0, display.Text.Length - 1);
    }
}