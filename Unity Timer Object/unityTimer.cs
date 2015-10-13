/*-----------------------------------------------------------------------------------------
 *  Unity Timer Object
 *  Coded By: Anthony McKeever (GitHub: Snip3rM00n)
 *  (2/19/2015)
 *
 *	Provides a timer object for the Unity Framework.  Implemented as an IDisposable so it
 *	can be disposed and garbage collected.
 *  
 *  Coded in: C# with Unity 5 and .Net 4.5.2 Frameworks
 *---------------------------------------------------------------------------------------*/

using UnityEngine;
using System;

public class unityTimer : IDisposable
{
    public double remainingTime;
    private double resetValue;

	private bool ticking = false;
	private bool timerElapsed = false;

	/// <summary>
	/// Returns if the timer is ticking.
	/// </summary>
	public bool Ticking { get { return ticking; } }

	/// <summary>
	/// Returns if the timer has elapsed.
	/// </summary>
	public bool TimerElapsed { get { return timerElapsed; } }

	#region Initializers

	/// <summary>
	/// Initializes the unityTimer object with default value of 30 seconds.
	/// </summary>
	public unityTimer()
    {
        Initialize();
    }

	/// <summary>
	/// Initializes the unityTimer object with a developer defined amount of seconds.
	/// </summary>
	/// <param name="timeToSet">An amount of seconds to initialize the timer at.</param>
	public unityTimer(double timeToSet)
    {
        Initialize(timeToSet);
    }

    private void Initialize(double timeToSet = 30f)
    {
        remainingTime = timeToSet;
        resetValue = timeToSet;
    }

    #endregion

    #region Work Methods

	/// <summary>
	/// Sets the remainingTime and the resetValue for the timer.
	/// </summary>
	/// <param name="timeToSet">The amount of seconds to set the timer to</param>
	public void SetTimer(double timeToSet)
	{
		remainingTime = timeToSet;
		resetValue = timeToSet;
	}

    /// <summary>
    /// Starts the Timer
    /// </summary>
    public void StartTimer()
    {
        ticking = true;
    }

    /// <summary>
    /// Stops the Timer
    /// </summary>
    public void StopTimer()
    {
        ticking = false;
    }

    /// <summary>
    /// Ticks the timer down using DeltaTime.
    /// </summary>
    public void Tick()
    {
        if (ticking)
            if (remainingTime > 0)
                remainingTime -= Time.deltaTime;
            else
            {
                StopTimer();
                timerElapsed = true;
            }
    }

    /// <summary>
    /// Resets the timer
    /// </summary>
    public void reset()
    {
        timerElapsed = false;
        remainingTime = resetValue;
    }

    /// <summary>
    /// Resets the timer
    /// </summary>
    /// <param name="stopTicking">Specifies if ticking should stop.</param>
    public void reset(bool stopTicking)
    {
        reset();

        if (stopTicking)
            StopTimer();
    }

    #endregion

    #region String Formaters

    /// <summary>
    /// Provides a string for the remaining time in the timer.
    /// </summary>
    /// <returns>Returns the time remaining without milliseconds.</returns>
	public override string ToString()
	{
		return Math.Round(remainingTime, 0).ToString();
	}

	/// <summary>
	/// Provides a string for the remaining time in the timer.
	/// </summary>
	/// <param name="ms">Integer representing how many millisecond places to return.</param>
	/// <returns>Returns the time remaining with ms millisecond places</returns>
	public string ToString(int millies)
	{
		return Math.Round(remainingTime, millies).ToString();
	}

	#endregion

	#region Disposal Code

	private bool disposed = false;

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                remainingTime = 0;
                resetValue = 0;
            }

            disposed = true;
        }
    }

    ~unityTimer()
    {
        Dispose(false);
    }
    
    #endregion
}
