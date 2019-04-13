const quizList = [
    {
        id: 1,
        code: 'CareerPathways',
        name: 'Career Pathways',
        type: 'range',
        desc: 'Learn about which roles meet your personal interests, education and career goals',
        questions: [
            {
                type: 'choice',
                text: 'Which of the following best describes your current role?',
                choices: [
                    {
                        text: 'EN',
                        score: 1,
                    },
                    {
                        text: 'RN',
                        score: 2,
                    },
                    {
                        text: 'RN/RM',
                        score: 3,
                    },
                    {
                        text: 'Studying to become an EN',
                        score: 4,
                    },
                    {
                        text: 'Studying to become an RN',
                        score: 5,
                    },
                    {
                        text: 'Non-nursing professional',
                        score: 6,
                    },
                ],
                attributes: [
                    {
                        attribute: 'current_role',
                        positive_score: true,
                    },
                ]
            },
            {
                type: 'choice',
                text: 'What is your highest level of nursing related education?',
                choices: [
                    {
                        text: 'Diploma or Cert IV',
                        score: 1,
                    },
                    {
                        text: 'Bachelor of Nursing Degree',
                        score: 2,
                    },
                    {
                        text: 'Post Grad Certificate',
                        score: 3,
                    },
                    {
                        text: 'Post Grad Dipolma',
                        score: 4,
                    },
                    {
                        text: 'Masters',
                        score: 5,
                    },
                    {
                        text: 'PhD',
                        score: 6,
                    },
                    {
                        text: 'None of the above',
                        score: 7,
                    },
                ],
                attributes: [
                    {
                        attribute: 'education_level',
                        positive_score: true,
                    },
                ]
            },
            {
                text: 'How important is it to work within business hours?',
                choices: [
                    {
                        text: 'Not important',
                        score: 0,
                    },
                    {
                        text: 'Moderately important',
                        score: 0.5,
                    },
                    {
                        text: 'Very important',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'has_family_friendly_hours',
                        positive_score: true,
                    },
                    {
                        attribute: 'has_no_family_friendly_hours',
                        positive_score: false,
                    },
                ]
            },
            {
                text: 'Would you prefer to work within or outside a hospital environment?',
                choices: [
                    {
                        text: 'Within hospital',
                        score: 0,
                    },
                    {
                        text: 'A mix of both',
                        score: 0.5,
                    },
                    {
                        text: 'Outside hospital',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'outside_hospital_environment',
                        positive_score: true,
                    },
                    {
                        attribute: 'within_hospital_environment',
                        positive_score: false,
                    },
                ]
            },
            {
                text: 'How comfortable do you feel about traveling every day to different locations and working in a range of environments in the community (for example, people\'s homes, workplaces, schools)',
                choices: [
                    {
                        text: 'Not comfortable',
                        score: 0,
                    },
                    {
                        text: 'Moderately comfortable',
                        score: 0.5,
                    },
                    {
                        text: 'Very comfortable',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'travel_working_range_large',
                        positive_score: true,
                    },
                    {
                        attribute: 'travel_working_range_small',
                        positive_score: false,
                    },
                ]
            },
            {
                text: 'Would you prefer to work a clearly defined specialty area or across a range of health needs and social conditions?',
                choices: [
                    {
                        text: 'Single defined speciality area',
                        score: 0,
                    },
                    {
                        text: 'Don\'t mind',
                        score: 0.5,
                    },
                    {
                        text: 'A range of health needs and social conditions',
                        score: 1,
                    },

                ],
                attributes: [
                    {
                        attribute: 'broad_work_range',
                        positive_score: true,
                    },
                ]
            },
            {
                text: 'How much does a negative perception (from your friends or nursing colleagues) towards a certain job influence your decision about which area/speciality you want to practice in?',
                choices: [
                    {
                        text: 'Very influential',
                        score: 0,
                    },
                    {
                        text: 'Moderately influence',
                        score: 0.5,
                    },
                    {
                        text: 'No influence',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'colleague_influence',
                        positive_score: false,
                    },
                ]
            },
            {
                text: 'How important is it for you to be in a strong position to directly influence the care of an individual and actively advocate for them?',
                choices: [
                    {
                        text: 'Not important',
                        score: 0,
                    },
                    {
                        text: 'Moderately important',
                        score: 0.5,
                    },
                    {
                        text: 'Very important',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'directly_influence_individual',
                        positive_score: true,
                    },
                ]
            },

            {
                text: 'How important is it for you to have the flexibility and scope of practice to empower your patients to take care of their own health?',
                choices: [
                    {
                        text: 'Not important',
                        score: 0,
                    },
                    {
                        text: 'Moderately important',
                        score: 0.5,
                    },
                    {
                        text: 'Very important',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'empower_client_health_care',
                        positive_score: true,
                    },
                ]
            },
            {
                text: 'How willing are you to navigate and drive your own career pathway?',
                choices: [
                    {
                        text: 'Not willing',
                        score: 0,
                    },
                    {
                        text: 'Moderately willing',
                        score: 0.5,
                    },
                    {
                        text: 'Very willing',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'navigate_career_path',
                        positive_score: true,
                    },
                ]
            },
            {
                text: 'How important is it that you earn a comparable salary to your hospital colleagues?',
                choices: [
                    {
                        text: 'Less important, I am willing to prioritise work/life balance or work in my preferred setting over pay',
                        score: 0,
                    },
                    {
                        text: 'Moderately important',
                        score: 0.5,
                    },
                    {
                        text: 'Very important',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'comparable_hospital_salary',
                        positive_score: true,
                    },
                ]
            },
            {
                text: 'How important is it that you work in a setting that you have had exposure to during your undergraduate training?',
                choices: [
                    {
                        text: 'Not important',
                        score: 0,
                    },
                    {
                        text: 'Moderately important',
                        score: 0.5,
                    },
                    {
                        text: 'Very important',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'exposure_during_training',
                        positive_score: true,
                    },
                ]
            },
            {
                text: 'How interested are you to work with socially disadvantaged and marginalised communities?',
                choices: [
                    {
                        text: 'Not interested',
                        score: 0,
                    },
                    {
                        text: 'Moderately interested',
                        score: 0.5,
                    },
                    {
                        text: 'Very interested',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'work_with_disadvantaged',
                        positive_score: true,
                    },
                ]
            },
            {
                text: 'How important is it to you to work in a setting that has national practice standards or guidelines specifically addressing your nursing area of practice? (This often indicates the nursing role is well established and has been well defined by the profession)',
                choices: [
                    {
                        text: 'Not important',
                        score: 0,
                    },
                    {
                        text: 'Moderately important',
                        score: 0.5,
                    },
                    {
                        text: 'Important',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'national_standards',
                        positive_score: true,
                    },
                ]
            },
            {
                text: 'How important is it to you to care for people across their lifespan, also known as "from cradle to the grave care" ?',
                choices: [
                    {
                        text: 'Not important',
                        score: 0,
                    },
                    {
                        text: 'Moderately important',
                        score: 0.5,
                    },
                    {
                        text: 'Very important',
                        score: 1,
                    },
                ],
                attributes: [
                    {
                        attribute: 'lifespan_care',
                        positive_score: true,
                    },
                ]
            },
        ],
        results: [
            {
                group: 'General Practice',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Nursing role is well established and has been defined by the profession.',
                        attribute: 'national_standards',
                    },
                    {
                        text: 'Abilty to engage in  care for people across their lifespan – "cradle to the grave"',
                        attribute: 'lifespan_care',
                    },
                    {
                        text: 'A single workplace environment everyday',
                        attribute: 'travel_working_range_small',
                    },
                ],
            },
            {
                group: 'Refugee / detention',
                attributes: [
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                    {
                        text: 'Nursing role is well established and has been defined by the profession.',
                        attribute: 'national_standards',
                    },
                    {
                        text: 'A single workplace environment everyday',
                        attribute: 'travel_working_range_small',
                    },
                ],
            },
            {
                group: 'Aged Care',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'A single workplace environment everyday',
                        attribute: 'travel_working_range_small',
                    },
                ],
            },
            {
                group: 'Community health / District nursing',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                ],
            },
            {
                group: 'Aboriginal Community Controlled Health Services',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                    {
                        text: 'Nursing role is well established and has been defined by the profession.',
                        attribute: 'national_standards',
                    },
                    {
                        text: 'Abilty to engage in  care for people across their lifespan – "cradle to the grave"',
                        attribute: 'lifespan_care',
                    },
                ],
            },
            {
                group: 'Correctional / Justice / Prison health',
                attributes: [
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Pay comparible with hospital colleagues',
                        attribute: 'comparable_hospital_salary',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                    {
                        text: 'Nursing role is well established and has been defined by the profession.',
                        attribute: 'national_standards',
                    },
                    {
                        text: 'A single workplace environment everyday',
                        attribute: 'travel_working_range_small',
                    },
                ],
            },
            {
                group: 'Drug and Alcohol',
                attributes: [
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                ],
            },
            {
                group: 'Immunisation',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'colleague_influence',
                        attribute: 'colleague_influence',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                ],
            },
            {
                group: 'Maternal / child health',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'colleague_influence',
                        attribute: 'colleague_influence',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                ],
            },
            {
                group: 'Men\'s Health',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                    {
                        text: 'Abilty to engage in  care for people across their lifespan – "cradle to the grave"',
                        attribute: 'lifespan_care',
                    },
                ],
            },
            {
                group: 'Mental Health',
                attributes: [
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                    {
                        text: 'Abilty to engage in  care for people across their lifespan – "cradle to the grave"',
                        attribute: 'lifespan_care',
                    },
                ],
            },
            {
                group: 'Work health & safety / Occupational health & safety',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'colleague_influence',
                        attribute: 'colleague_influence',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                ],
            },
            {
                group: 'Public health',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                    {
                        text: 'Nursing role is well established and has been defined by the profession.',
                        attribute: 'national_standards',
                    },
                    {
                        text: 'Abilty to engage in  care for people across their lifespan – "cradle to the grave"',
                        attribute: 'lifespan_care',
                    },
                ],
            },
            {
                group: 'Primary or secondary school',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                ],
            },
            {
                group: 'Remote area nursing',
                attributes: [
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'Work outside of hospital environment',
                        attribute: 'outside_hospital_environment',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                    {
                        text: 'Abilty to engage in  care for people across their lifespan – "cradle to the grave"',
                        attribute: 'lifespan_care',
                    },
                ],
            },
            {
                group: 'Sexual and reproductive health',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                ],
            },
            {
                group: 'Women\'s health',
                attributes: [
                    {
                        text: 'Family Friendly Hours',
                        attribute: 'has_family_friendly_hours',
                    },
                    {
                        text: 'Empower patients to take care of own health',
                        attribute: 'empower_client_health_care',
                    },
                    {
                        text: 'A range of environments in the community',
                        attribute: 'travel_working_range_large',
                    },
                    {
                        text: 'Works across a range of health needs and social conditions',
                        attribute: 'broad_work_range',
                    },
                    {
                        text: 'Ability to navigate your own career pathway',
                        attribute: 'navigate_career_path',
                    },
                    {
                        text: 'Ability to directly influence the care of an individual and actively advocate for them',
                        attribute: 'directly_influence_individual',
                    },
                    {
                        text: 'Ability to work with socially disadvantaged and marginalised communities',
                        attribute: 'work_with_disadvantaged',
                    },
                    {
                        text: 'Abilty to engage in  care for people across their lifespan – "cradle to the grave"',
                        attribute: 'lifespan_care',
                    },
                ],
            },
            {
                group: 'Hospital based nursing',
                attributes: [
                    {
                        text: 'colleague_influence',
                        attribute: 'colleague_influence',
                    },
                    {
                        text: 'Pay comparible with hospital colleagues',
                        attribute: 'comparable_hospital_salary',
                    },
                    {
                        text: 'Nursing role is well established and has been defined by the profession.',
                        attribute: 'national_standards',
                    },
                    {
                        text: 'Work inside of hospital environment',
                        attribute: 'within_hospital_environment',
                    },
                    {
                        text: 'Shift work hours ',
                        attribute: 'has_no_family_friendly_hours',
                    },
                    {
                        text: 'A single workplace environment everyday',
                        attribute: 'travel_working_range_small',
                    },
                ],
            },
        ]
    },
    {
        id: 2,
        code: 'SkillsAssessment',
        name: 'Skills Self Assessment',
        type: 'range',
        desc: 'Learn about which roles meet your personal interests, education and career goals',
        questions: [

            {
                group: 'intro_domain',
                group_name: '',
                type: 'choice',
                text: 'Which of the following best describes your current role?',
                choices: [
                    {
                        text: 'EN',
                        score: 1,
                    },
                    {
                        text: 'RN',
                        score: 2,
                    },
                    {
                        text: 'RN/RM',
                        score: 3,
                    },
                    {
                        text: 'Studying to become an EN',
                        score: 4,
                    },
                    {
                        text: 'Studying to become an RN',
                        score: 5,
                    },
                    {
                        text: 'Non-nursing professional',
                        score: 6,
                    },
                ],
                attributes: [
                    {
                        attribute: 'current_role',
                        positive_score: true,
                    },
                ]
            },
            {
                group: 'intro_domain',
                group_name: '',
                type: 'choice',
                text: 'What is your highest level of nursing related education?',
                choices: [
                    {
                        text: 'Diploma or Cert IV',
                        score: 1,
                    },
                    {
                        text: 'Bachelor of Nursing Degree',
                        score: 2,
                    },
                    {
                        text: 'Post Grad Certificate',
                        score: 3,
                    },
                    {
                        text: 'Post Grad Dipolma',
                        score: 4,
                    },
                    {
                        text: 'Masters',
                        score: 5,
                    },
                    {
                        text: 'PhD',
                        score: 6,
                    },
                    {
                        text: 'None of the above',
                        score: 7,
                    },
                ],
                attributes: [
                    {
                        attribute: 'education_level',
                        positive_score: true,
                    },
                ]
            },


            {
                group: 'direct_care',
                group_name: 'Direct Care',
                text: 'How do you apply evidence-based clinical skills to your everyday practice?',
                choices: [
                    {
                        text: 'I am learning how to apply and carry out clinical care that is holistic, person-centred and culturally safe.',
                        score: 0,
                    },
                    {
                        text: 'I am confidently practicing clinical care that is holistic, person-centred and culturally safe.',
                        score: 1,
                    },
                    {
                        text: 'I lead and guide others to confidently practice clinical care that is holistic, person-centred and culturally safe.',
                        score: 2,
                    },
                ],
                examples: [
                    'Conduct triage training in own area of practice',
                    'Use validated risk screening tools as part of the assessment process',
                    'Conducts mental health screening with individuals identified as “at risk” of harm to self or others',
                ],
                attributes: [
                    {
                        attribute: 'ssa_1_1',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'direct_care',
                group_name: 'Direct Care',
                text: 'How do you align your skills with the health needs of the population?',
                choices: [
                    {
                        text: 'I’m learning how to participate in campaigns aimed at addressing relevant public health issues within the local primary health care setting.',
                        score: 0,
                    },
                    {
                        text: 'I am confident in participating in public health strategies aligned to the local setting and community and work collaboratively with others to improve health and avoid hospitalisation.',
                        score: 1,
                    },
                    {
                        text: 'I lead by actively contributing to the development and implementation of public health campaigns and strategies relevant to local, regional or national needs.',
                        score: 2,
                    },
                ],
                examples: [
                    'Regularly review aggregated clinical data using a clinical audit tool (e.g. Pencat) to target “at risk” populations',
                    'Care is provided within a multidisciplinary team to maximise health outcomes for your community',
                    'Collaborate with local PHNs to design and implement programs based on evidence-based guidelines, aligned with identified local population needs',
                ],
                attributes: [
                    {
                        attribute: 'ssa_1_4',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'direct_care',
                group_name: 'Direct Care',
                text: 'When you work within your scope of practice, to what extent do you accept responsibility and professional accountability?',
                choices: [
                    {
                        text: 'I’m learning to utilise and apply critical thinking to explore and analyse evidence in clinical practice.',
                        score: 0,
                    },
                    {
                        text: 'I confidently embed critical thinking in clinical decision making in practice.',
                        score: 1,
                    },
                    {
                        text: 'I lead by utilising critical thinking to inform advanced  clinical decision making in practice ',
                        score: 2,
                    },
                ],
                examples: [
                    'Practice inline with the National Practice Standards for Nurses in General Practice',
                    'Delegate responsibility to members of the nursing team',
                    'Co-ordinates the nursing team in the delivery of quality health care',
                ],
                attributes: [
                    {
                        attribute: 'ssa_1_5',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'direct_care',
                group_name: 'Direct Care',
                text: 'How do you promote the cultural/religious/sexual preferences of the clients you take care of?',
                choices: [
                    {
                        text: 'I’m learning to deliver care and support that respects the dignity, wishes and beliefs of all individuals.',
                        score: 0,
                    },
                    {
                        text: 'I’m confident to act as a role model to provide non-judgemental, value-based care and expect and promote these values to other team members.',
                        score: 1,
                    },
                    {
                        text: 'I act as a guide for values-based care. I lead the professional development and quality improvement strategies to ensure they reflect a values-based approach to care.',
                        score: 2,
                    },
                ],
                examples: [
                    'Acknowledge and facilitate cultural practices in recognising how cultural practices can impact on recovery',
                    'Display a compassionate and caring approach when working with individuals seeking care',
                    'Challenge stigma and discrimination within your organisation',
                ],
                attributes: [
                    {
                        attribute: 'ssa_1_7',
                        positive_score: true,
                    },
                ],
            },


            {
                group: 'education',
                group_name: 'Education',
                text: 'When it comes to education, I want to feel competent and confident in my scope of practice, so I…',
                choices: [
                    {
                        text: 'I’m learning how to be responsible and accountable for keeping my knowledge and skills up to date through continuing professional development and participating in clinical support strategies e.g. mentoring, coaching, clinical supervision.',
                        score: 0,
                    },
                    {
                        text: 'I confidently expand my scope of practice to meet the needs of those accessing care by undertaking appropriate education and skill development.',
                        score: 1,
                    },
                    {
                        text: 'I act as a leader by proactively seeking to expand and maintain advanced scope of practice by undertaking appropriate education and skill development.',
                        score: 2,
                    },
                ],
                examples: [
                    'Focuses CPD on increasing clinical skills to ensure care delivery is in line with needs of those accessing care and the broader community.',
                    'Uses own professional judgement about an identified activity which is beyond own capacity or scope of practice and initiates consultation with, or referral to, other members of the health care team',
                    'Undertakes postgraduate qualifications to optimise scope of practice and increase opportunities to lead improvements in primary health care  ',
                ],
                attributes: [
                    {
                        attribute: 'ssa_2_1',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'education',
                group_name: 'Education',
                text: 'How do you incorporate learning, teaching and assessment** into your nursing role? **These may include: peer learning, knowledge about evidence-based initiatives and appraisals',
                choices: [
                    {
                        text: 'I’m learning the importance of sharing information and external learning with other members of the primary health care team.',
                        score: 0,
                    },
                    {
                        text: 'I’m learning the importance of sharing information and external learning with other members of the primary health care team.',
                        score: 1,
                    },
                    {
                        text: 'I lead and guide members of the primary health care team by providing formal education relating to evidence-based initiatives and processes.',
                        score: 2,
                    },
                ],
                examples: [
                    'Share new clinical based learnings with members of the healthcare team e.g. during clinical team meetings',
                    'Acts as preceptor for new staff',
                    'Initiates and facilitates learning opportunities within nurse networks',
                ],
                attributes: [
                    {
                        attribute: 'ssa_2_2',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'education',
                group_name: 'Education',
                text: 'How do you encourage a rich learning environment?',
                choices: [
                    {
                        text: 'I’m learning how to apply relevant professional standards and guidelines into teaching and learning experiences.',
                        score: 0,
                    },
                    {
                        text: 'I can confidently identify relevant professional standards and clinical guidelines to support teaching and learning experiences.',
                        score: 1,
                    },
                    {
                        text: 'I play a lead role in ensuring the application of standards and guidelines that underpin a quality teaching and learning experience.',
                        score: 2,
                    },
                ],
                examples: [
                    'As mentor, focus on the goals and professional development needs of the mentoree',
                    'Apply the NMBA’s “Enrolled nurse standards for practice” (2016) when acting as a supervisor',
                    'Apply educational based theories in the development of learning modules ‎for colleagues',
                ],
                attributes: [
                    {
                        attribute: 'ssa_2_3',
                        positive_score: true,
                    },
                ],
            },

            {
                group: 'research',
                group_name: 'Research',
                text: 'How do you support continuous quality improvement in all aspects of service delivery?',
                choices: [
                    {
                        text: 'I’m learning how to participate in quality improvement activities to evaluate the quality and effectiveness of nursing care.',
                        score: 0,
                    },
                    {
                        text: 'I’m confident to conduct quality improvement activities to identify quality issues within the clinical setting.',
                        score: 1,
                    },
                    {
                        text: 'I lead activities within the primary health care team around quality improvement related to nursing care.',
                        score: 2,
                    },
                ],
                examples: [
                    'Applies tools for quality measurement and improvement (eg. PDSA cycles)',
                    'Uses clinical data aggregation tool/s to understand local population health needs',
                    'Suggests evidence-based health care improvements',
                ],
                attributes: [
                    {
                        attribute: 'ssa_3_1',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'research',
                group_name: 'Research',
                text: 'How do you participate in research and its evaluation?',
                choices: [
                    {
                        text: 'I’m learning how to recognise the ethical implications of an audit, research project or clinical trials.',
                        score: 0,
                    },
                    {
                        text: 'I’m confident in my understanding of the principles of research governance.',
                        score: 1,
                    },
                    {
                        text: 'I lead by ensuring frameworks for research governance are applied appropriately.',
                        score: 2,
                    },
                ],
                examples: [
                    'Understanding the research methodology used in research project currently running in your area of practice',
                    'Identify and utilise skills and knowledge of staff to support or undertake research related activity such as audit, evaluation, and wider research for benefit of the organisation',
                    'Contribute to the wider research agenda through initiating or supporting research activity',
                ],
                attributes: [
                    {
                        attribute: 'ssa_3_2',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'research',
                group_name: 'Research',
                text: 'How do you identify opportunities and/or funding sources for quality improvement through research and evaluation?',
                choices: [
                    {
                        text: 'I’m learning how to seek information about opportunities for funding research activities.',
                        score: 0,
                    },
                    {
                        text: 'I can confidently identify opportunities for funding or additional resources to support evaluation activities or research.',
                        score: 1,
                    },
                    {
                        text: 'I act as a leader in clinical policy and research communities by identifying deficits in evidence and potential funding sources for practice or research development.',
                        score: 2,
                    },
                ],
                examples: [
                    'Identifies ideas for research activities',
                    'Identifies opportunities and/or funding for research opportunities',
                    'Involvement in clinical policy and research communities to identify deficits in evidence and identification of potential funding sources for practice or research development',
                ],
                attributes: [
                    {
                        attribute: 'ssa_3_3',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'research',
                group_name: 'Research',
                text: 'How does evidence-based research support your nursing practice?',
                choices: [
                    {
                        text: 'I’m learning how to demonstrate knowledge of current policies and procedures and an understanding of their implications for nursing practice.',
                        score: 0,
                    },
                    {
                        text: 'I confidently demonstrate sound understanding of all current policies and procedures and an understanding of their implications for nursing practice.',
                        score: 1,
                    },
                    {
                        text: 'I lead and guide the primary health care team in their understanding of current policies and procedures and their evidence-base.',
                        score: 2,
                    },
                ],
                examples: [
                    'Use the latest National Immunisation Schedule to guide immunisation practice.',
                    'Know how to access research databases to find information to inform your practice.',
                    'Sharing the latest clinical guidelines related to your area of practice e.g. obesity management with others in your primary health care team.',
                ],
                attributes: [
                    {
                        attribute: 'ssa_3_4',
                        positive_score: true,
                    },
                ],
            },



            {
                group: 'professional_leadership',
                group_name: 'Professional Leadership',
                text: 'How do you develop and implement models of care to improve care delivery?',
                choices: [
                    {
                        text: 'I’m learning how to use local care pathways to apply timely access to appropriate care.',
                        score: 0,
                    },
                    {
                        text: 'I confidently contribute to the development or improvement of local care pathways, encouraging team members and, where possible, Individuals to contribut.',
                        score: 1,
                    },
                    {
                        text: 'I lead and work collaboratively to develop and evaluate the care pathways utilised by the primary health care team.',
                        score: 2,
                    },
                ],
                examples: [
                    'Ensure care delivery is clearly documented and provided as a summary at points of care transitions',
                    'Support local PHN in increasing the efficiency and effectiveness of medical services for individuals seeking care',
                    'Support systems that enhance interagency collaboration',
                ],
                attributes: [
                    {
                        attribute: 'ssa_4_1',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'professional_leadership',
                group_name: 'Professional Leadership',
                text: 'How do you support a culture of effective teamwork?',
                choices: [
                    {
                        text: 'I’m learning how to adopt an innovative approach to identifying new ways of working.',
                        score: 0,
                    },
                    {
                        text: 'I’m confident in my ability to mentor and support colleagues to embrace an innovative approach to the development of the service.',
                        score: 1,
                    },
                    {
                        text: 'I lead by influencing service development by supporting and developing innovative and lateral thinking in self and others.',
                        score: 2,
                    },
                ],
                examples: [
                    'Works in partnership with colleagues',
                    'Practice reflects the organisation\'s shared values and beliefs',
                    'Take responsibility for own area of work',
                ],
                attributes: [
                    {
                        attribute: 'ssa_4_3',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'professional_leadership',
                group_name: 'Professional Leadership',
                text: 'To what extent do you develop, implement or review policies and procedures?',
                choices: [
                    {
                        text: 'I’m learning the importance of developing clinical assessment policies tailored to my own area of practice.',
                        score: 0,
                    },
                    {
                        text: 'I’m confident to contribute to the development of guidelines and policy locally, regionally and nationally, where appropriate.',
                        score: 1,
                    },
                    {
                        text: 'I lead on local, regional or national primary health care nursing policies and strategies to deliver quality care.',
                        score: 2,
                    },
                ],
                examples: [
                    'Own clinical practice is informed by policies aligned with best practice guidelines',
                    'Contribute at an organisational level to development of policies and procedures',
                    'In conjunction with my local primary health network, contribute to policy development related to care coordination and service delivery',
                ],
                attributes: [
                    {
                        attribute: 'ssa_4_4',
                        positive_score: true,
                    },
                ],
            },
            {
                group: 'professional_leadership',
                group_name: 'Professional Leadership',
                text: 'How do you contribute to change in clinical practice and systems?',
                choices: [
                    {
                        text: 'I’m learning how to seek opportunities to improve the service, by generating ideas for innovation.',
                        score: 0,
                    },
                    {
                        text: 'I’m confident in my role as a change agent.',
                        score: 1,
                    },
                    {
                        text: 'I lead the application of theoretical perspectives of change management to create an environment for successful change and practice development.',
                        score: 2,
                    },
                ],
                examples: [
                    'Initiates changes to service delivery in line with changes to MBS billing with area of practice',
                    'Facilitate change opportunities through application of collaborative methodology e.g. Model for Improvement',
                    'Identify gap between recommended practice and current practice (baseline assessment) to identify actions needed to implement the change',
                ],
                attributes: [
                    {
                        attribute: 'ssa_4_5',
                        positive_score: true,
                    },
                ],
            },



            {
                group: 'support_systems',
                group_name: 'Support Systems',
                text: 'How do you engage in professional and organisational leadership?',
                choices: [
                    {
                        text: 'I’m learning how to build professional networks promoting exchange of knowledge, skills and resources.',
                        score: 0,
                    },
                    {
                        text: 'I’m confident to support peers to develop networks and share information.',
                        score: 1,
                    },
                    {
                        text: 'I lead and guide the establishment of professional networks with peers from the interdisciplinary team and promote exchange of knowledge, skills and resources.',
                        score: 2,
                    },
                ],
                examples: [
                    'Seeks leadership opportunities within own area of practice',
                    'Undertakes CPD with a focus on leadership skills',
                    'Leads programs of work focusing on the expanding the role of nursing in primary health care',
                ],
                attributes: [
                    {
                        attribute: 'ssa_5_1',
                        positive_score: true,
                    },
                ],
            },

        ],
        results: [
            {
                group: 'Some SSA Group #1',
                attributes: [
                    {
                        text: 'Test SSA #1',
                        attribute: 'ssa_3_1',
                    },
                    {
                        text: 'Test SSA #2',
                        attribute: 'ssa_3_2',
                    },
                ],
            },
        ]
    },
];

var segmentData = [
    {
        group: 'education',
        name: 'Education',
        score: 1,
        label: '',
        levels: [
            {
                value: 1,
                completed: true,
            },
            {
                value: 1,
                completed: false,
            },
            {
                value: 1,
                completed: false,
            },
        ],
        value: 1,
    },
    {
        group: 'research',
        name: 'Research',
        score: 1,
        label: '',
        levels: [
            {
                value: 1,
                completed: true,
            },
            {
                value: 1,
                completed: true,
            },
            {
                value: 1,
                completed: false,
            },
        ],
        value: 1,
    },
    {
        group: 'professional_leadership',
        name: 'Publication_& Professional_Leadership',
        score: 1,
        label: '',
        levels: [
            {
                value: 1,
                completed: true,
            },
            {
                value: 1,
                completed: true,
            },
            {
                value: 1,
                completed: true,
            },
        ],
        value: 1,
    },
    {
        group: 'support_systems',
        name: 'Support_of Systems',
        score: 1,
        label: '',
        levels: [
            {
                value: 1,
                completed: true,
            },
            {
                value: 1,
                completed: true,
            },
            {
                value: 1,
                completed: false,
            },
        ],
        value: 1,
    },
    {
        group: 'direct_care',
        name: 'Direct_Care',
        score: 1,
        label: '',
        levels: [
            {
                value: 1,
                completed: true,
            },
            {
                value: 1,
                completed: true,
            },
            {
                value: 1,
                completed: false,
            },
        ],
        value: 1,
    },
];

const actions = [
    {
        group: 'Direct Care',
        group_desc: 'Access education targeting areas specific to primary health care.',
        action_list: [
            'APNA Chronic Disease and Healthy Ageing workshops',
            'APNA elearning portal: What’s New in Asthma Management',
            'APNA elearning portal: Asthma Fundamentals for Primary Health Care Nurses',
        ],
    },
    {
        group: 'Education',
        group_desc: 'Incorporate evidence-based tools into your care delivery.',
        action_list: [],
    },
    {
        group: 'Support',
        group_desc: 'Consider joining your local APNA Nurse Network..',
        action_list: [],
    },
    {
        group: 'Research',
        group_desc: 'Become familiar with quality improvement tools and examples of when to use them.',
        action_list: [],
    },
    {
        group: 'Professional Leadership',
        group_desc: 'Complete the online module, Leadership in action.',
        action_list: [],
    },
];


const Data = {
    quizList,
    segmentData,
    actions,
};

export default Data