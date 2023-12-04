using System;
using System.Collections.Generic;

public static class ServiceLocator  
{  
    // 서비스들을 저장하는 Dictionary. Type을 키로 하고, 서비스 객체를 값으로 함.
    private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();  

    /// <summary>
    /// 서비스를 등록하는 메서드.
    /// 제네릭 타입 T를 사용하여 어떤 타입의 서비스든 등록할 수 있음.
    /// </summary>
    /// <param name="service">등록할 서비스 객체</param>
    public static void RegisterService<T>(T service)  
    {  
        // 서비스 객체를 Dictionary에 추가. 이미 존재하는 경우 해당 타입의 서비스를 덮어씀.
        Services[typeof(T)] = service;  
    }  

    /// <summary>
    /// 등록된 서비스를 검색하는 메서드.
    /// 제네릭 타입 T를 사용하여 원하는 타입의 서비스를 검색할 수 있음.
    /// </summary>
    /// <returns>검색된 서비스 객체. 해당 타입의 서비스가 없으면 예외 발생.</returns>
    public static T GetService<T>()  
    {  
        // 요청된 타입의 서비스를 Dictionary에서 찾아 반환.
        // 해당 타입의 서비스가 없는 경우 예외가 발생할 수 있음.
        return (T)Services[typeof(T)];  
    }  
}