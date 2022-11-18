export function IsValidPhone(phoneInput) {
    let phone = phoneInput.split('-'); 
    if (phone.length != 5 && phone[0][0] != '+'){
        return false;
    }        
    if (isNaN(phone[0][1])) {
        return false;
    }
    for(let i = 1;i < 5; i++){
        if (isNaN(phone[i])) {
            return false;
        }
    }
    return true;      
}

export function IsValidDate (date){
    if (!/\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z/.test(date)) return false;
    const d = new Date(date);
    return d instanceof Date && !isNaN(d) && d.toISOString()===date; 
}