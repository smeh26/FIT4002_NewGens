# Career Pathways Quiz

## Data structure

Questions:

```json
{
  id: 0,
  text: 'How important is it that you have a job with family friendly hours? ie within buisness hours',
  resultText: 'Shifts within business hours'
}
```

resultText is the 'attribute' text when looking at results.



## Scoring

Prototype took an attribute approach where a set of attributes were created and each question was associated an attribute. 

Another possibility is to have the ideal answer array for each sector and generate a difference value for each one. 

If the first option is better then the scoreId (or attribute) would be necessary, otherwise it is not and only the question id matters.





## Questions

1. Tell us about yourself...are you a:
   - 1=EN
   - 2=RN
   - 3= RN/RM
   - 4= NP
   - 5=Studying to become an EN
   - 6=Studying to become an RN
   - 7=Non-nursing professional
2. What is your highest level of nursing education completed?
   - 1=Diploma or Cert IV
   - 2=Bachelor of Nursing Degree
   - 3=Post Grad Certificate
   - 4=Post Grad Dipolma
   - 5=Masters
   - 6=PhD
   - 7=Hospital trained
   - 8=None of the above
3. What kinds of people do you enjoy working with?
   - 1=babies and children
   - 2=Youth
   - 3=Women
   - 4=Men
   - 5=Elderly
   - 6=Marginalised and disavantaged communities
   - 7=all of the above
4. How important is it that you have a job with family friendly hours? ie within buisness hours
   - 1 = very important
   - 0.5 = moderately important
   - 0 = not important
5. How important is it for you to have the flexibility to empower people to take care of their own health?
   - 1 = very important
   - 0.5 = moderately important
   - 0 = not important
6. Would you prefer to work within or outside a hospital environment?
   - 1 = outside hospital
   - 0.5 = a mix of both
   - 0 = within hospital
7. How willing are you to travel and work in a range of environments in the community eg schools, people's homes etc)
   - 1 = very comfortable
   - 0.5 = moderately comfortable
   - 0 = not comfortable
8. How much does the perception of your nursing colleagues influence your decision about which area/speciality you want to practice in?
   - 1 = no influence
   - 0.5 = moderate influence
   - 0 = very influential
9. How willing are you to navigate and drive your own career pathway?
   - 1 = very willing
   - 0.5 = moderately willing
   - 0 = not willing
10. Would you like to directly influence the care of an individual and actively advocate for them?
  - 1 = Yes
  - 0.5 = moderate value
  - 0 = No
11. How willing are you to accept lower pay as a  trade off for better work/life balance?
    - 1 = Very willing
    - 0.5 = moderately willing
    - 0 = not at all willing
12. How important is it to you to work in a setting where the nursing role is well established and has  national practice standards in place?
    - 1 = very important
    - 0.5 = moderately important
    - 0 = not important
13. How important is it to work in a setting that limits the amount of physical stress to your body?
    - 1= very important
    - 0.5=moderately important
    - 0=not at all important



## Sectors

The list of sectors followed by the ideal answer list for that sector.

Question 1,2 and 3 are not included and question 13 is not defined, so the list is question 4 to 12

1. General Practice

   1	1	1	0	1	1	1	1	0

2. Refugee / detention

   0.5	1	1	0	1	1	1	1	0

3. Aged Care

   0.5  1	1	0	1	1	1	1	1

4. Community health / District nursing

   1	1	1	1	1	1	1	1	1

5. Aboriginal Community Controlled Health Services

   1	1	1	1	1	1	1	1	0

6. Correctional / Justice / Prison health

   0	1	1	0	1	1	1	0	0

7. Drug and Alcohol

   0.5	1	0.5	1	1	1	1	?	1

8. Immunisation

   1	1	1	1	0	1	1	?	?

9. Maternal / child health

   1	1	1	1	0	1	1	?	1

10. Men's Health

  1	1	0.5	1	1	1	1	?	1

11. Mental Health

    0.5	1	0.5	1	1	1	1	?	1

12. Work health & safety / Occupational health & safety

    1	1	1	1	0	1	1	?	?

13. Public health

    1	1	1	1	1	1	1	?	0

14. Primary or secondary school

    1	1	1	1	1	1	1	?	?

15. Remote area nursing

    0	1	1	1	1	1	1	?	1

16. Sexual and reproductive health

    1	1	0.5	1	1	1	1	?	1

17. Women's health

    1	1	0.5	1	1	1	1	?	1

18. Hospital based nursing

    0	0	0	0	0	0	0	0	1