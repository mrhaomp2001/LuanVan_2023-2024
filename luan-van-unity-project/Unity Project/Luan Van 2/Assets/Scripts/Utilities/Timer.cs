using System;
using System.Collections;
using UnityEngine;

namespace Library
{
    public class Timer
    {
        private static ArrayList timerIds = new ArrayList();
        private static int countTimerId = 0;
        //Class event timer
        public class EventTimer{
            public int count;
            public TimerId source;
        }
        //Thuc hien cong viec sau khoang thoi gian
        public static TimerId PerformWithDelay(double miliSeconds, Action<EventTimer> callback, int loop = 1, MonoBehaviour monoBehaviour = null)
        {
            float timeStart = Time.time;
            TimerId timerId = new TimerId(monoBehaviour, miliSeconds, callback, loop, new TimerId.MetaData(countTimerId, timeStart));
            if (miliSeconds > 0) {
                void _callback(int count)
                {
                    int index = timerId.__metadata.GetIndex();
                    TimerId source = (TimerId)timerIds[index];
                    source.__metadata.SetCountLoop();
                    callback(new EventTimer { count = source.__metadata.GetCountLoop(), source = source });
                    if (source.__metadata.GetCountLoop() == source.loop)
                    {
                        source.__metadata.SetIsCancelled();
                    }
                    timerIds[index] = source;
                }
                if (monoBehaviour != null)
                {
                    timerId.__timerId = monoBehaviour.StartCoroutine(CoroutineDelay(miliSeconds, _callback, loop));
                }
                else
                {
                    timerId.__timerId = CoroutineHandler.StartStaticCoroutine(CoroutineDelay(miliSeconds, _callback, loop));
                }
            }
            //Add timer to array
            timerIds.Add(timerId);
            countTimerId = countTimerId + 1;
            //
            if (miliSeconds <= 0)
            {
                if (callback != null)
                {
                    int index = timerId.__metadata.GetIndex();
                    TimerId source = (TimerId)timerIds[index];
                    callback(new EventTimer { count = source.__metadata.GetCountLoop(), source = source });
                }
            }
            return timerId;
        }
        private static IEnumerator CoroutineDelay(double miliSeconds, Action<int> callback, int loop = 1, double miliSecondsFirst = 0)
        {
            int countLoop = 0;
            float secondsFirst = (float)miliSecondsFirst / 1000;
            float seconds = (float)miliSeconds/1000;
            while (loop <= 0 || countLoop < loop){
                if (secondsFirst > 0 && countLoop == 0){
                    yield return new WaitForSeconds(secondsFirst);
                }else if (seconds > 0){
                    yield return new WaitForSeconds(seconds);
                }
                countLoop = countLoop + 1;
                if (callback != null){
                    callback(countLoop);
                }
            } 
        }

        //Pause timer    
        public static void Pause(TimerId _timerId)
        {
            if (_timerId != null) {
                int index = _timerId.__metadata.GetIndex();
                TimerId timerId = (TimerId) timerIds[index];          
                if (timerId != null && timerId.__metadata.isPause() == false && timerId.__metadata.isCancelled() == false){
                    timerId.__metadata.SetIsPause(true);
                    timerId.__metadata.SetTimePause(Time.time);
                    if (timerId.__timerId != null)
                    {
                        if (timerId.monoBehaviour != null)
                        {
                            timerId.monoBehaviour.StopCoroutine(timerId.__timerId);
                        }
                        else
                        {
                            CoroutineHandler.StopStaticCoroutine(timerId.__timerId);
                        }
                        timerId.__timerId = null;
                    }         
                    timerIds[index] = timerId;
                }else{
                    Debug.LogFormat("Timer with index {0} can't pause", index);
                }
            }
            
        }
        //Resume timer
        public static void Resume(TimerId _timerId)
        {
            if (_timerId != null) {
                int index = _timerId.__metadata.GetIndex();
                TimerId timerId = (TimerId) timerIds[index];
                if (timerId != null && timerId.__metadata.isPause() == true && timerId.__metadata.isCancelled() == false){              
                    timerId.__metadata.SetIsPause(false);
                    double miliSeconds = timerId.miliSeconds - (timerId.__metadata.calcTimePause()*1000);   
                    int loop = timerId.loop;
                    if (timerId.loop > 0)
                    {
                        loop = timerId.loop - timerId.__metadata.GetCountLoop();
                        if (loop < 1)
                        {
                            loop = 1;
                        }
                    }
                    void _callback(int count)
                    {
                        TimerId source = (TimerId)timerIds[index];
                        source.__metadata.SetCountLoop();
                        timerId.callback(new EventTimer { count = source.__metadata.GetCountLoop(), source = source });
                        if (source.__metadata.GetCountLoop() == source.loop)
                        {
                            source.__metadata.SetIsCancelled();
                        }
                        timerIds[index] = source;
                    }
                    if (timerId.monoBehaviour != null)
                    {
                        timerId.__timerId = timerId.monoBehaviour.StartCoroutine(CoroutineDelay(timerId.miliSeconds, _callback, loop, miliSeconds));
                    }
                    else
                    {
                        timerId.__timerId = CoroutineHandler.StartStaticCoroutine(CoroutineDelay(timerId.miliSeconds, _callback, loop, miliSeconds));
                    }
                    timerIds[index] = timerId;
                }
                else{
                    Debug.LogFormat("Timer with index {0} can't resume", index);
                }
            }
        }
        //Huy timer
        public static void Cancel(TimerId _timerId)
        {
            if (_timerId != null) {
                int index = _timerId.__metadata.GetIndex();
                TimerId timerId = (TimerId) timerIds[index];
                if (timerId != null && timerId.__metadata.isCancelled() == false){
                    if (timerId.__timerId != null)
                    {
                        if (timerId.monoBehaviour != null)
                        {
                            timerId.monoBehaviour.StopCoroutine(timerId.__timerId);
                        }
                        else
                        {
                            CoroutineHandler.StopStaticCoroutine(timerId.__timerId);
                        }
                    }
                    timerId.__metadata.SetIsCancelled();     
                    timerIds[index] = timerId;    
                }    
            } 
        }
    }

    public class TimerId {
        public class MetaData {
            private int index;
            private float timeStart;
            private float timePause;
            private bool _isPause = false;
            private bool _isCancelled = false;
            private int countLoop = 0;
            //
            public MetaData(int index, float timeStart)
            {
                this.index = index;
                this.timeStart = timeStart;
            }
            public void SetIsPause(bool isPause)
            {
                this._isPause = isPause;
            }
            public bool isPause()
            {
                return this._isPause;
            }
            public void SetIsCancelled()
            {
                this._isCancelled = true;
            }
            public bool isCancelled()
            {
                return this._isCancelled;
            }
            public void SetTimePause(float timePause)
            {
                this.timePause = timePause;
            }
            public float calcTimePause()
            {
                if (!this.timePause.Equals(null)){
                    return this.timePause - this.timeStart;
                }
                return 0;
            }
            public void SetCountLoop()
            {
                this.countLoop = this.countLoop + 1;
            }
            public int GetCountLoop()
            {
                return this.countLoop;
            }
            public int GetIndex()
            {
                return this.index;
            }
        }
        //Variables
        public readonly MonoBehaviour monoBehaviour;
        public Coroutine __timerId;
        public MetaData __metadata;
        public readonly double miliSeconds;  
        public readonly int loop;
        public readonly Action<Timer.EventTimer> callback;    
        
        //Method
        public TimerId(MonoBehaviour monoBehaviour, double miliSeconds, Action<Timer.EventTimer> callback, int loop, MetaData metadata)
        {
            this.monoBehaviour = monoBehaviour;
            this.miliSeconds = miliSeconds;
            this.callback = callback;
            this.loop = loop;
            this.__metadata = metadata;
        }
    }
}