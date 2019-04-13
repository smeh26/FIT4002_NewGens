#!/bin/sh

# run first
ansible-playbook -i hosts aws_launch.yml -e "env=prod"
ansible-playbook -i ec2.py aws_provision.yml -e "env=prod"
ansible-playbook -i ec2.py aws_deploy.yml -e "env=prod"