namespace Workshop01.Backend;

public class Time
{
    private int _hour;
    private int _minute;
    private int _second;
    private int _millisecond;

    public Time()
    {
        _hour = 0;
        _minute = 0;
        _second = 0;
        _millisecond = 0;
    }

    public Time(int hour)
    {
        Hour = hour;
    }

    public Time(int hour, int minute)
    {
        Hour = hour;
        Minute = minute;
    }

    public Time(int hour, int minute, int second)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    public Time(int hour, int minute, int second, int millisecond)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = millisecond;
    }

    public int Hour
    {
        get => _hour;
        set => _hour = ValidHour(value);
    }

    public int Minute
    {
        get => _minute;
        set => _minute = ValidMinute(value);
    }

    public int Second
    {
        get => _second;
        set => _second = ValidSecond(value);
    }

    public int Millisecond
    {
        get => _millisecond;
        set => _millisecond = ValidMillisecond(value);
    }
    
    public override string ToString()
    {
        int hour12 = Hour % 12;
        if (hour12 == 0)
            hour12 = 12;
        return $"{hour12:D2}:{Minute:D2}:{Second:D2}.{Millisecond:D3} {(Hour >= 12 ? "PM" : "AM")}";
    }

    private int ValidHour(int hour)
    {
        if (hour < 0 || hour > 23)
        {
            throw new Exception( $"The hour: {hour}, is not valid.");
        }
        
        return hour;
    }

    private int ValidMinute(int minute)
    {
        if (minute < 0 || minute >= 59)
        {
            throw new ArgumentOutOfRangeException(nameof(minute), "Los minutos deben estar entre 0 y 59.");
        }
        return minute;
    }

    private int ValidSecond(int second)
    {
        if (second < 0 || second >= 59)
        {
            throw new ArgumentOutOfRangeException(nameof(second), "Los segundos deben estar entre 0 y 59.");
        }
        return second;
    }

    private int ValidMillisecond(int millisecond)
    {
        if (millisecond < 0 || millisecond >= 999)
        {
            throw new ArgumentOutOfRangeException(nameof(millisecond), "Los milisegundos deben estar entre 0 y 999.");
        }
        return millisecond;
    }

    public Time Add(Time other)
    {
        int millisecond = Millisecond + other.Millisecond;
        int secondCarry = 0;

        if (millisecond >= 1000)
        {
            secondCarry = millisecond / 1000;
            millisecond %= 1000;
        }

        int second = Second + other.Second + secondCarry;
        int minuteCarry = 0;

        if (second >= 60)
        {
            minuteCarry = second / 60;
            second %= 60;
        }

        int minute = Minute + other.Minute + minuteCarry;
        int hourCarry = 0;

        if (minute >= 60)
        {
            hourCarry = minute / 60;
            minute %= 60;
        }

        int hour = Hour + other.Hour + hourCarry;

        if (hour >= 24)
        {
            hour %= 24;
        }

        return new Time(hour, minute, second, millisecond);
    }

    public bool IsOtherDay(Time other)
    {
        int millisecond = Millisecond + other.Millisecond;
        int secondCarry = millisecond / 1000;

        int second = Second + other.Second + secondCarry;
        int minuteCarry = second / 60;

        int minute = Minute + other.Minute + minuteCarry;
        int hourCarry = minute / 60;

        int hour = Hour + other.Hour + hourCarry;

        return hour >= 24;
    }

    public int ToMinutes()
    {
        return Hour * 60 + Minute;
    }

    public int ToSeconds()
    {
        return (Hour * 60 + Minute) * 60 + Second;
    }
    public int ToMilliseconds()
    {
        return ((Hour * 60 + Minute) * 60 + Second) * 1000 + Millisecond;
    }

}
