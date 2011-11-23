using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework.UIControls
{
    public enum ControlAction
    {
        CascadingMove
    }

    public class ControlActionGroup
    {
        private List<ControlInfo> _controls = new List<ControlInfo>();
        private ControlAction _currentAction;
        private float _xDist;
        private float _yDist;
        private float _duration;
        private float _timeStartedAction;

        public ControlActionGroup(IEnumerable<BaseControl> controls)
        {
            _controls = controls.Select(control => new ControlInfo { Control = control }).ToList();
        }

        public void BeginAction(ControlAction action, float xDist, float yDist, float duration)
        {
            _currentAction = action;
            _xDist = xDist;
            _yDist = yDist;
            _duration = duration;
            _timeStartedAction = SMH.Now;

            foreach (ControlInfo info in _controls)
            {
                info.Started = info.Finished = false;
                info.StartX = info.Control.X;
                info.StartY = info.Control.Y;
            }
        }

        public bool Update(float dt)
        {
            int controlCount = 0;
            foreach (ControlInfo info in _controls)
            {
                if (_currentAction == ControlAction.CascadingMove)
                {
                    if (!info.Started && SMH.TimePassed(_timeStartedAction, (float)controlCount * 0.15f))
                    {
                        info.Started = true;
                        info.TimeStarted = SMH.Now;
                    }

                    if (info.Started && !info.Finished)
                    {
                        info.Control.X += (_xDist / _duration) * dt;
                        info.Control.Y += (_yDist / _duration) * dt;

                        //Make sure that when the controls are coming up from off the screen they don't go past their 
                        //intended end point. This can happen sometimes when returning to the menu because the game is 
                        //processing a bunch of shit in one frame
                        if (_yDist < 0 && info.Control.Y < info.StartY + _yDist) info.Control.Y = info.StartY + _yDist;

                        if (SMH.TimePassed(info.TimeStarted, _duration))
                        {
                            info.Finished = true;
                            info.Control.X = info.StartX + _xDist;
                            info.Control.Y = info.StartY + _yDist;
                            if (controlCount == _controls.Count)
                            {
                                //If the last control is finished, then return true because the action is finished.
                                return true;
                            }
                        }
                    }
                }
                controlCount++;
            }
            return false;
        }

        private class ControlInfo
        {
            public BaseControl Control { get; set; }
            public bool Started { get; set; }
            public bool Finished { get; set; }
            public float StartX { get; set; }
            public float StartY { get; set; }
            public float TimeStarted { get; set; }
        };
    }
}
