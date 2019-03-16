using UnityEngine;
using UnityEditor;
using System.Timers;

namespace UnityEngine
{
    public abstract class WorldSpace : MonoBehaviour
    {
        public abstract void IntervalUpdate();
        public void Awake()
        {
            StockExchange.StartExchange();
            Timer sTimer = new Timer();
            sTimer.Elapsed += new ElapsedEventHandler((object source, ElapsedEventArgs e) => { IntervalUpdate(); StockExchange.calculateExchange(); });
            sTimer.Interval = 1000; // 1000 ms is one second
            sTimer.Start();
        }
        
    }
}