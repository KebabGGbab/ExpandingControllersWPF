﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;


namespace ExpandingControllersWPF
{
    [ContentProperty("Value")]
    public partial class NumericUpDown : TextBox
    {
        #region property
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        public static readonly DependencyProperty StepProperty;
        public static readonly DependencyProperty RatingProperty;
        #endregion

        #region accessor
        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public decimal MinValue
        {
            get { return (decimal)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public decimal MaxValue
        {
            get { return (decimal)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public decimal Step
        {
            get { return (decimal)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        public byte Rating
        {
            get { return (byte)GetValue(RatingProperty); }
            set { SetValue(RatingProperty, value); }
        }

        #endregion

        static NumericUpDown()
        {
            ValueProperty = DependencyProperty.Register("Value", typeof(decimal), typeof(NumericUpDown), new FrameworkPropertyMetadata(0.0M, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged, CoerceValue), ValidateValue);
            MinValueProperty = DependencyProperty.Register("MinValue", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(0.0M));
            MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(100.0M));
            StepProperty = DependencyProperty.Register("Step", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(1.0M));
            RatingProperty = DependencyProperty.Register("Rating", typeof(byte), typeof(NumericUpDown), new PropertyMetadata((byte)0));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private static object CoerceValue(DependencyObject d, object baseValue)
        {
            NumericUpDown control = (NumericUpDown)d;
            if (baseValue is not decimal value)
                return DependencyProperty.UnsetValue;
            if (value < control.MinValue)
                return control.MinValue;
            if (value > control.MaxValue)
                return control.MaxValue;
            return value;
        }

        private static bool ValidateValue(object value)
        {
            return value is decimal;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("UpValue") is RepeatButton UpButton && GetTemplateChild("DownValue") is RepeatButton DownButton)
            {
                UpButton.Click += UpValue_Click;
                DownButton.Click += DownValue_Click;
            }
        }

        private void UpValue_Click(object sender, RoutedEventArgs e)
        {
            Value++;
        }

        private void DownValue_Click(object sender, RoutedEventArgs e)
        {
            Value--;
        }
    }
}
