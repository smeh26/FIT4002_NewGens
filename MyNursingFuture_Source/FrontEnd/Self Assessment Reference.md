# Data structure

Each question:

```json
{
  id: 0,
  text: 'How do you apply evidence-based clinical skills to your everyday practice?',
  type: 'range', // range or choice
  aspectId: 1, // the aspect id this question is scoring
  domainId: 1 // the domain of the aspect id (maybe not neccessary if the domains of aspects can be reliably identified)
}
```



# Scoring

Every aspect is placed in one of: 

- Foundation ( x <= 0.4)
- Intermediate (x > 0.4 <= 0.7)
- Advanced (x > 0.7)

Those levels are guesses and may be changed and should they be configurable?

Domain scores are an average of all aspect scores in that domain.

# Domains (RN)

## Clinical Care (1)

Aspects:

1. Assessment and management

   **Question**: How do you apply evidence-based clinical skills to your everyday practice?

   **Answers**:

   - I am learning how to apply and carry out clinical care that is holistic, person-centred and culturally safe.
   - I am confidently practicing clinical care that is holistic, person-centred and culturally safe.
   - I lead and guide others to confidently practice clinical care that is holistic, person-centred and culturally safe.

2. Planning care

   **Question**: How do you use evidence-based clinical skills when planning care?

   **Answers**:

   - I’m learning to use data to identify individuals with chronic disease risks
   - I can confidently identify, use and monitor local data that identifies individuals with chronic disease risks 
   - I work with individuals and local communities to guide a health care approach that supports and maximizes the population’s health and wellbeing whilst leading the primary health care team to build community capacity 

3. Evaluation of care

   **Question**:  How do you use evidence-based clinical skills to measure the impact of your care on clients?

   **Answers**:

   - I’m learning to utilise and apply critical thinking to explore and interpret evidence in clinical practice
   - I confidently utilise enhanced critical thinking to explore and analyse evidence in clinical practice
   - I lead by using advanced critical thinking skills to evaluate and apply evidence in clinical practice and guide the primary health care team to ensure high level assessment and decision making

4. Population health

   **Question**:  How do you align your skills with the health needs* of the population?

   *These needs may include delivering targeted public health, health promotion and prevention.*

   **Answers**: 

   - I’m learning how to participate in campaigns aimed at addressing relevant public health issues within the local primary health care setting
   - I am confident in participating in public health strategies aligned to the local setting and community and work collaboratively with others to improve health and avoid hospitalisation
   - I lead by actively contributing to the development and implementation of public health campaigns and strategies relevant to local, regional or national needs

5. Accountability and responsibility

   **Question**:  How do you accept responsibility and professional accountability within your scope of practice?

   **Answers**:

   - I’m learning to utilise and apply critical thinking to explore and analyse evidence in clinical practice
   - I confidently embed critical thinking in clinical decision making in practice
   - I lead by utilising critical thinking to inform advanced  clinical decision making in practice 

6. Health literacy

   **Question**: How do you consider the health
   literacy of the client when talking to
   them and their healthcare team?

   **Answers**:

   - I’m learning to recognise the impact of low health literacy 
   - I am confident in my understanding of the impact of health literacy as an essential component of culturally safe care
   - I lead the delivery of culturally safe care by ensuring the primary health care team understand the impact of poor health literacy

7. Value-based care

   **Question**: How do you practice
   non-discriminatory and
   non-judgemental care?

   **Answers**:

   - I’m learning to deliver care and support that respects the dignity, wishes and beliefs of all individuals
   - I’m confident to act as a role model to provide non-judgemental, value-based care and expect and promote these values to other team members
   - I act as a guide for values-based care. I lead the professional development and quality improvement strategies to ensure they reflect a values-based approach to care 

8. Culturally safe practice (A&TS specific)

   **Question**: Do you practice culturally safe care for
   all Aboriginal and Torres Strait Islander
   people accessing care?

   **Answers**:

   - I’m learning the importance of demonstrating respect and providing culturally safe care for Aboriginal and Torres Strait Islander people in the community
   - I am confident to take an active role in ensuring the primary health care service is culturally safe for Aboriginal and Torres Strait Islander people
   - I lead and guide the primary health care team to deliver culturally safe care for Aboriginal and Torres Strait Islander people

9. Culturally safe practice

   **Question**:  When taking care of a patient, how do
   you consider cultural and language
   barriers?

   **Answers**:

   - I’m learning the importance of demonstrating respect and providing culturally safe care for culturally and linguistically diverse people in the community 
   - I confidently I take an active role in ensuring the primary health care service is culturally safe for culturally and linguistically diverse people 
   - I lead and guide the primary health care team to deliver culturally safe care for culturally and linguistically diverse people 

10. Personal reflection

   **Question**:  In clinical practice, how do you apply
   personal and professional reflection?

   **Answers**:

   - I’m learning to participate in a clinical support network within the primary health care team 
   - I confidently evaluate outcomes from my own clinical reflection and contribution to a clinical support network 
   - I guide and monitor the involvement and outcomes of members of the primary health care team in clinical support networks to support professional reflection

11. Teamwork

    **Question**: As part of a primary healthcare team,
    how do you use your knowledge to
    assess and manage risk?

    **Answers**:

    - I’m learning to identify and document possible risks to individuals accessing care and colleagues
    - I can confidently manage risk to individuals accessing care and colleagues, whilst remaining accountable for the care provided to these individuals
    - I lead the design and implementation of strategies for risk management to protect individuals accessing care and colleagues 

12. Tech literacy

    **Question**: How do you use technology* in your
    everyday practice?
    **information technology, clinical software and decision support tools.*

    **Answers**:

    - I’m learning to use and apply available software or data management systems to improve the ability to identify individuals with, or at risk of, chronic diseases
    - I’m confidently using my skills to identify, implement and evaluate the use of new technologies in health care delivery
    - I lead by ensuring data capture systems are fit for purpose



## Education (2)

Aspects:

1. Scope of practice

   **Question**: How do you tailor your education to
   increase competence and confidence
   in your scope of practice?

   **Answers**:

   - I’m learning how to be responsible and accountable for keeping my knowledge and skills up to date through continuing professional development and participating in clinical support strategies e.g. mentoring, coaching, clinical supervision
   - I confidently expand my scope of practice to meet the needs of those accessing care by undertaking appropriate education and skill development
   - I act as a leader by proactively seeking to expand and maintain advanced scope of practice by undertaking appropriate education and skill development

2. Learning, teaching and assessment

   **Question**: How do you incorporate learning,
   teaching and appraisal* into your
   nursing role?
   **These may include: peer learning, knowledge about*
   *evidence-based initiatives and appraisals.*

   **Answers**:

   - I’m learning the importance of sharing information and external learning with other members of the primary health care team 
   - I’m confident when teaching colleagues and students through informal and/or formal education
   - I lead and guide members of the primary health care team by providing formal education relating to evidence-based initiatives and processes

3. Learning environment

   **Question**: How do you contribute to an
   environment that cultivates learning?

   **Answers**:

   - I’m learning how to apply relevant professional standards and guidelines into teaching and learning experiences 
   - I can confidently identify relevant professional standards and clinical guidelines to support teaching and learning experiences
   - I play a lead role in ensuring the application of standards and guidelines that underpin a quality teaching and learning experience

4. Community education

   **Question**: How do you work with the community
   to identify and share information
   relevant to their healthcare needs?

   **Answers**:

   - I’m learning to work with the community to identify health and support needs 
   - I’m confident to consult and collaborate with the community to identify health and support needs
   - I lead the development of collaborations with the community to ensure local and national public health needs

## Research (3)

Aspects:

1. Quality improvement

   **Question**: How do you support a culture of
   continuous quality improvement in all
   aspects of service delivery?

   **Answers**:

   - I’m learning how to participate in quality improvement activities to evaluate the quality and effectiveness of nursing care 
   - I’m confident to conduct quality improvement activities to identify quality issues within the clinical setting
   - I lead activities within the primary health care team around quality improvement related to nursing care 

2. Research and evaluation

   **Question**: How do you participate in
   research and its evaluation?

   **Answers**:

   - I’m learning how to recognise the ethical implications of an audit, research project or clinical trials
   - I’m confident in my understanding of the principles of research governance 
   - I lead by ensuring frameworks for research governance are applied appropriately

3. Finding resources and support

   **Question**: How do you identify the opportunities
   and resources needed for quality
   improvement, research and
   evaluation?

   **Answers**:

   - I’m learning how to seek information about opportunities for funding research activities
   - I can confidently identify opportunities for funding or additional resources to support evaluation activities or research 
   - I act as a leader in clinical policy and research communities by identifying deficits in evidence and potential funding sources for practice or research development 

4. Evidence-based practice

   **Question**: How does evidence-based research
   underpin your nursing practise?

   **Answers**:

   - I’m learning how to demonstrate knowledge of current policies and procedures and an understanding of their implications for nursing practice 
   - I confidently demonstrate sound understanding of all current policies and procedures and an understanding of their implications for nursing practice 
   - I lead and guide the primary health care team in their understanding of current policies and procedures and their evidence-base

## Optimizing health systems (4)

Aspects:

1. Care coordination

   **Question**: How do you develop and implement
   models of care to
   improve care delivery?

   **Answers**:

   - I’m learning how to use local care pathways to apply timely access to appropriate care
   - I confidently contribute to the development or improvement of local care pathways, encouraging team members and, where possible, Individuals to contribute
   - I lead and work collaboratively to develop and evaluate the care pathways utilised by the primary health care team 

2. Effective communication

   **Question**: How do you promote effective
   communication to improve the way
   primary healthcare teams work
   together?

   **Answers**:

   - I’m learning how to recognise effective verbal and non-verbal communication techniques and how to apply these across a variety of situations
   - I have confidence in my ability to promote and implement the use of effective communication techniques
   - I lead by mentoring and supporting the use of effective communication techniques in the primary health care team and with service users

3. Effective teamwork

   **Question**:  How do you support a culture of
   effective teamwork?

   **Answers**:

   - I’m learning how to adopt an innovative approach to identifying new ways of working
   - I’m confident in my ability to mentor and support colleagues to embrace an innovative approach to the development of the service
   - I lead by influencing service development by supporting and developing innovative and lateral thinking in self and others 

4. Policies and procedures

   **Question**: To what extent do you develop,
   implement or review policies and
   procedures?

   **Answers**:

   - I’m learning the importance of developing clinical assessment policies tailored to my own area of practice
   - I’m confident to contribute to the development of guidelines and policy locally, regionally and nationally, where appropriate 
   - I lead on local, regional or national primary health care nursing policies and strategies to deliver quality care

5. Drive change

   **Question**:  How do you contribute to change
   within the nursing profession?

   **Answers**:

   - I’m learning how to seek opportunities to improve the service, by generating ideas for innovation
   - I’m confident in my role as a change agent
   - I lead the application of theoretical perspectives of change management to create an environment for successful change and practice development

## Leadership (5)

Aspects:

1. Leadership

   **Question**: How do you engage in professional
   leadership within and beyond your own
   setting?

   **Answers**:

   - I’m learning how to build professional networks promoting exchange of knowledge, skills and resources 
   - I’m confident to support peers to develop networks and share information
   - I lead and guide the establishment of professional networks with peers from the interdisciplinary team and promote exchange of knowledge, skills and resources

2. Professional relationships

   **Question**: How do you build and maintain
   relationships to contribute to the
   improvement of the nursing
   profession?

   **Answers**:

   - I’m learning how to provide collegial support to ENs and RNs in other settings
   - I’m confident to identify and disseminate information relevant to ENs and RNs through existing networks, such as the APNA Nurse Network
   - I lead the development of communities of practice and networks, to disseminate resources and practice initiatives 

3. Share knowledge

   **Question**: How do you share nursing expertise
   with other health professionals?

   **Answers**:

   - I’m learning how to share research findings with colleagues 
   - I’m confident to share research findings through local bulletins, team meetings, forums and/or professional journals 
   - I lead and guide the dissemination of scholarly activity and new developments to support the integration of evidence based practice and influence the development of the learning environment

4. Promote nursing

   **Question**: How do you promote the role
   of the nurse in primary health care?

   **Answers**:

   - I’m learning how to promote nursing in primary health care to other nurses and health professionals, individuals and other relevant groups
   - I’m confident to engage and recruit individuals and organisations to advocate for the role of the primary health care nurse in the broader health care system
   - I lead through collaborating proactively with public health agencies and local authorities to ensure primary health care nursing is actively engaged in the health improvement strategies for the local community

5. Being a resource

   **Question**: To what extent do you act as a
   knowledge resource for others*?
   *these may include the community, committees and other health professionals beyond your own setting*

   **Answers**:

   - I am learning how to identify an area of particular interest and provide insight into the contemporary evidence that supports practice
   - I’m confident to work towards an area of clinical expertise including undertaking continuing professional development (CPD) activities
   - I am a leader in at least one area of practice and am seen as a local expert to articulate the most contemporary evidence, and approaches to practice and management