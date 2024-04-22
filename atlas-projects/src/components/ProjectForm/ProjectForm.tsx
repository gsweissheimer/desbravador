import React, { useEffect, useState } from 'react';
import { Layout, Form, Input, Button, DatePicker, Select, Col, Row } from 'antd';
import { FormInstance } from 'antd/lib/form';
import moment, { Moment } from 'moment';
import axios from 'axios';
import { useDispatch } from "react-redux";
import { setUser, setProject, fetchUserFromAPI, fetchProjectFromAPI } from "../../actions/atlasActions";
import { EnumType, ProjectDTO, ProjectRisk, ProjectStatus, enumToEnumTypeArray, ProjectRiskTitle, ProjectStatusTitle, AtlasState, User } from '../../types';
import { useSelector } from 'react-redux';

const { Content } = Layout;
const { Option } = Select;
const ProjectForm: React.FC = () => {
    
    
  const [form] = Form.useForm();
    const users = useSelector((state: AtlasState) => state.atlas.users);
    const projects = useSelector((state: AtlasState) => state.atlas.projects);
    
    const [selectedUsers, setSelectedUsers] = useState<User[]>([]);
    const [selectedUserId, setSelectedUserId] = useState<number>();
    const [selectedUser, setSelectedUser] = useState<User>();
    const [insertedData, setInsertedData] = useState<User>();
    const [usersAvailables, setUsersAvailables] = useState<User[]>();
    const [otherUsersAvailables, setOtherUsersAvailables] = useState<User[]>();
    const [risk, setRisk] = useState<string>(ProjectRisk.LOW_RISK);
    const [status, setStatus] = useState<string>(ProjectStatus.UNDER_ANALYSIS);

    const riskOptions: EnumType[] = enumToEnumTypeArray(ProjectRisk,ProjectRiskTitle);
    const statusOptions: EnumType[] = enumToEnumTypeArray(ProjectStatus,ProjectStatusTitle);
  
    const handleUserChange = (value: number) => {
      setSelectedUserId(value);
      setSelectedUser(users.filter((item:User) => item.id === value)[0])
    };

    const handleProjectStatuskChange = (value: string) => {
      setStatus(value)
    }

    const handleProjectRiskChange = (value: string) => {
      setRisk(value)
    }
  
    const handleProjectResponsableChange = (id: number) => {
        setOtherUsersAvailables(users)
        setSelectedUsers([])
        setOtherUsersAvailables(users.filter((item:User) => item.id !== id))
    };
  
    const handleAssignUser = () => {
      if (selectedUser && !selectedUsers.find((item:User) => item.id === selectedUserId)) {
        setSelectedUsers([...selectedUsers, users.filter((item:User) => item.id === selectedUserId)[0]]);
      }
    };
    const dispatch = useDispatch();
  
    const onFinish = (values: any) => {

      const requestBody : ProjectDTO = {
        ...values,
        responmsable: selectedUser,
        users: selectedUsers
      };

      axios.post('http://localhost:5130/api/Project', requestBody, {
        headers: {
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*'
        }
      })
        .then(response => {
          setInsertedData(response.data)
        })
        .catch(error => {
          console.error('Error:', error);
        });
        handleCleanForm()
    };

    const handleCleanForm = () => {
      form.resetFields(); 
      setSelectedUsers([])
    };


    useEffect(() => {
      setStatus(ProjectStatus.UNDER_ANALYSIS)
      setRisk(ProjectRisk.LOW_RISK)
    })

    useEffect(() => {
        setUsersAvailables(users)
    },[users])

    useEffect(() => {
        setOtherUsersAvailables(usersAvailables)
    },[usersAvailables])

    useEffect(() => {
      if (insertedData) {
        dispatch(setProject([...projects, insertedData]));
      }
    },[insertedData])
    
  return (
    <Layout style={{ padding: '0 24px 24px' }}>
      <Content
        className="site-layout-background"
        style={{
          padding: 24,
          margin: 0,
          minHeight: 280,
        }}
      >
        <h2>Adicionar Projeto</h2>
        <Form
          form={form}
          layout="vertical"
          onFinish={onFinish}
        >
          <Form.Item
            label="Nome"
            name="name"
            rules={[{ required: true, message: 'Por favor, insira o nome do projeto' }]}
          >
            <Input placeholder="Nome" />
          </Form.Item>
          <Form.Item
            label="Descrição"
            name="description"
            rules={[{ required: true, message: 'Por favor, insira a descrição do projeto' }]}
          >
            <Input.TextArea placeholder="Descrição" />
          </Form.Item>
        <Row gutter={16}>
          <Col span={12}> 
            <Form.Item
              label="Data de Início"
              name="startDate"
            >
              <DatePicker style={{ width: '100%' }} />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
                label="Data de Término"
                name="endDate"
            >
                <DatePicker style={{ width: '100%' }} />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}> 
            <Form.Item
                label="Status"
                name="status"
                rules={[{ required: true, message: 'Por favor, selecione o status do projeto' }]}
            >
                <Select
                  style={{ width: '100%' }}
                  onChange={handleProjectStatuskChange}
                  value={status}
                  placeholder='Selecione o status do projeto'
                  >
                    {statusOptions.map(option => (
                        <Option key={option.key} value={option.key}>
                            {option.value}
                        </Option>
                    ))}
                </Select>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
                label="Risco"
                name="risk"
                rules={[{ required: true, message: 'Por favor, selecione o risco do projeto' }]}
            >
                <Select
                  style={{ width: '100%' }}
                  onChange={handleProjectRiskChange}
                  value={risk}
                  placeholder='Selecione o risco do projeto'
                >
                    {riskOptions.map(option => (
                        <Option key={option.key} value={option.key}>
                            {option.value}
                        </Option>
                    ))}
                </Select>
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={24}> 
            <Form.Item
                label="Responsável"
                name="responmsable"
                rules={[{ required: true, message: 'Por favor, escolha o responsável do projeto' }]}
            >
                <Select
                    style={{ width: '100%' }} 
                    onChange={handleProjectResponsableChange}
                    placeholder="Selecione um responsável">
                        {usersAvailables && usersAvailables.map(user => (
                                <Option key={user.id} value={user.id}>
                                    {user.firstName + ' ' + user.lastName}
                                </Option>
                            ))}
                </Select>
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16} style={{alignItems: 'flex-end'}}>
          <Col span={20}> 
            <Form.Item
                label="Usuários"
                name="users"
            >
                <Select
                style={{ width: '100%' }}
                placeholder="Selecione um usuário"
                key={otherUsersAvailables?.length}
                onChange={handleUserChange}
                >
                {otherUsersAvailables && otherUsersAvailables.map(user => (
                        <Option key={user.id} value={user.id}>
                            {user.firstName + ' ' + user.lastName}
                        </Option>
                    ))}
                </Select>
            </Form.Item>
          </Col>
          <Col span={4}>
            <Form.Item>
                <Button type="primary" onClick={handleAssignUser} style={{ marginTop: '10px' }}>Atribuir</Button>
            </Form.Item>
          </Col>
            <div className="project-users">
            <h3>Usuários Selecionados:</h3>
            <ul>
                {selectedUsers.map(user => (
                <li key={user.id}>{user.firstName + ' ' + user.lastName}</li>
                ))}
            </ul>
            </div>
        </Row>
        <Row gutter={16}>
          <Col span={24}> 
            <Form.Item>
                <Button type="primary" htmlType="submit">Salvar Projeto</Button>
            </Form.Item>
          </Col>
        </Row>
        </Form>
      </Content>
    </Layout>
    );
};

export default ProjectForm;