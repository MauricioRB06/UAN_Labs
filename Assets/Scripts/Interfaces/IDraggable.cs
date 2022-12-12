
// Copyright (c) 2022 MauricioRB06 <https://github.com/MauricioRB06>
// Supervisory Teacher: herrera78 <https://github.com/herrera78>
//
// The Purpose Of This Script Is:
//
// Sets the behavior of objects that can be dragged.
//
// Documentation and References:
//
// C# Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/
//
// Last Update: 11.12.2022 By MauricioRB06

namespace Interfaces
{
    public interface IDraggable
    {
        void OnStartDrag();
        void OnEndDrag();
    }
}
