using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Data;
using Microsoft.Xna.Framework.Input;
using Smiley.Lib.Enums;

namespace Smiley.Lib.UI.Controls
{
    public class Slider : BaseControl
    {
        private const int NumBars = 6;
        private const int BarSpacing = 3;
        private const int BarHeight = 21;
        private const int BarWidth = 93;
        private const int SliderWidth = BarWidth + BarSpacing * 2;
        private const int SliderHeight = BarSpacing + (BarSpacing + BarHeight) * NumBars;

        #region Private Variables

        private bool _mousePressed;
        private int _minValue;
        private int _maxValue;
        private int _currentValue;
        private int _barsToDraw;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new Slider.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public Slider(float x, float y, int minValue, int maxValue)
        {
            X = x;
            Y = y;
            _minValue = minValue;
            _maxValue = maxValue;
            _barsToDraw = NumBars;
        }

        #endregion

        #region Public Methods

        public int Value
        {
            get { return _currentValue; }
            set
            {
                if (value >= _minValue && value <= _maxValue)
                {
                    _currentValue = value;
                    _barsToDraw = Convert.ToInt32((float)_currentValue / (float)_maxValue * (float)NumBars);
                }
            }
        }

        public int Width
        {
            get { return SliderWidth; }
        }

        public int Height
        {
            get { return SliderHeight; }
        }

        public override void Draw()
        {
            for (int i = 0; i < NumBars; i++)
            {
                if (i < _barsToDraw)
                {
                    SMH.Graphics.DrawSprite(Sprites.SliderBar, X + BarSpacing, (Y + SliderHeight) - (BarHeight + BarSpacing) * (i + 1));
                }
            }
        }

        public override void Update(float dt)
        {
            //Mouse was clicked inside slider
            if (SMH.Input.IsDown(Input.Attack) && SMH.Input.Cursor.Y < Y + SliderHeight && SMH.Input.Cursor.Y > Y && SMH.Input.Cursor.X > X && SMH.Input.Cursor.X < X + SliderWidth)
            {
                _mousePressed = true;
            }
            else if (!SMH.Input.IsDown(Input.Attack))
            {
                _mousePressed = false;
            }

            if (_mousePressed)
            {
                _barsToDraw = Convert.ToInt32(((Y + SliderHeight) - SMH.Input.Cursor.Y) / (BarSpacing + BarHeight));
                _currentValue = Convert.ToInt32((float)_barsToDraw / (float)NumBars * (float)_maxValue);
            }
        }

        #endregion
    }
}
