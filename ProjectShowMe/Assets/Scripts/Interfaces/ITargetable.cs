// <copyright file="ITargetable.cs" company="Bas de Koningh BV">
// Copyright (c) 2019 All Rights Reserved
// </copyright>
// <author>Bas de Koningh</author>
// <date>10/15/2019 12:50:58 PM </date>
using Humans;
 
public interface ITargetable 
{
    HumanType GetHumanType();
    void SetHumanType(HumanType humanType);
    void Lock();
    void UnLock();
    void Remove();
}
