export class AgeCalculation {
    public GetResult (dateOfBirth:Date):any {
        var today = new Date();
        var a = (today.getFullYear() * 100 + today.getMonth()) * 100 + today.getDay();
        var b = (dateOfBirth.getFullYear() * 100 + dateOfBirth.getMonth()) * 100 + dateOfBirth.getDay();
        var result = (a - b) / 10000;
        var n = result.toFixed();
        return n;
    }
}