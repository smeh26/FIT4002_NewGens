import React from 'react'
import { Button, Modal } from 'semantic-ui-react'

const ResultsExplanationModal = () => (
    <Modal trigger={<Button>Results Explanation</Button>}>
        <Modal.Header>Results Explanation</Modal.Header>
        <Modal.Content>
            <h1>Education</h1>
            <p>Practice that involves the enhancement of the knowledge and skills
                of health professionals and the community, by gaining and sharing
                knowledge and experience to promote quality health care and improve
                health outcomes. This involves recognition of existing knowledge,
                skills and capacity, engaging communities to promote wellness and
                manage illness and informal and formal education to health professionals
                and the community. Participation in self-directed education is recognised.
            </p>
            <h2>Level 1: RN (Registered Nurse)</h2>
            <p>You are currently working at the level expected of a registered nurse.
                Download the report for a detailed view of your answers.</p>
        </Modal.Content>
    </Modal>
)

export default ResultsExplanationModal