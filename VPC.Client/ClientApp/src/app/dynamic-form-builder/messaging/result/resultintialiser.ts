// export class ResultInitialiser {
//     // static getInstance(context: Object, name: string, ...args: any[])  {
//     //     // var instance = Object.create(context[name].prototype);
//     //     // instance.constructor.apply(instance, args);
//     //     // return <T> instance;


        
//     // }
//    getInstance(className: string, ...instanceparameters: any[]):any  {
//         var newInstance = Object.create(window[className].prototype);
//         newInstance.constructor.apply(newInstance, instanceparameters);
//         return newInstance;
//     }
//     constructor(){

//     }

// }


export class ResultInitialiser {
    getInstance<T>(context: Object, name: string, ...args: any[]) : T {
        var instance = Object.create(context[name].prototype);
        instance.constructor.apply(instance, args);
        return <T> instance;
    }
}

export interface IMethod {
    name: string;
    GetResult():string;
}