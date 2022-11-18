'use strict'

import './style.css';
import {IsValidDate, IsValidPhone} from './utils';

function ShowTime(date) {
    if (typeof date !== 'string')
    date = date.toISOString();
    return date;
}


//Add Call
const url = "http://localhost:5000/api";
const form = document.getElementById("formAddCall");
const inputPhoneFrom = form.querySelector("#inputPhoneFrom");
const inputPhoneTo = form.querySelector("#inputPhoneTo");
const inputTimeStart = form.querySelector("#inputTimeStart");
const inputTimeEnd = form.querySelector("#inputTimeEnd");
const submitBtn = form.querySelector("#submitAddFormAddCall");

let dateNow = ShowTime(new Date);
inputPhoneFrom.value = "+1-111-111-11-11";
inputPhoneTo.value = "+2-222-222-22-22"
inputTimeStart.value = dateNow;
inputTimeEnd.value = dateNow;

form.addEventListener('submit', submitAddFormAddCall);

async function submitAddFormAddCall(event)
{
    event.preventDefault();

    submitBtn.disabled = true;

    if(IsValidPhone(inputPhoneFrom.value) && 
    IsValidPhone(inputPhoneTo.value) && 
    IsValidDate(inputTimeStart.value) &&
    IsValidDate(inputTimeEnd.value)) {
        const call = {
            "FromPhone" : inputPhoneFrom.value.trim(),
            "ToPhone" : inputPhoneTo.value.trim(),
            "TimeStart" : inputTimeStart.value,
            "TimeEnd" : inputTimeEnd.value
        }

        try{
            let response = await fetch(url+'/call/create', {
                method: 'PUT',
                headers: {
                  'Content-Type': 'application/json;charset=utf-8',
                //    'Access-Control-Allow-Origin' : 'http://localhost:3000',
                //     mode: 'no-cors'
    
                },
                body: JSON.stringify(call)
            });
            let result = await response.json();
        } catch (e) {
            console.log(e.message);
        }
        
        
        //console.log(result.message);
        inputPhoneFrom.value = "+1-111-111-11-11";
        inputPhoneTo.value = "+2-222-222-22-22"
        inputTimeStart.value = dateNow;
        inputTimeEnd.value = dateNow;
        submitBtn.disabled = false;    
    }
}