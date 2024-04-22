import React, { useEffect, useState } from 'react';
import { Layout, Menu, Modal } from 'antd';
import {
  UserOutlined,
  LaptopOutlined,
} from '@ant-design/icons';
import { useDispatch } from "react-redux";
import { setUser, setProject, fetchUserFromAPI, fetchProjectFromAPI } from "../actions/atlasActions";
import {} from "../store/atlasReducer";


import ProjectForm from "../components/ProjectForm/ProjectForm";
import ListProjects from "../components/ListProjects/ListProjects";
import './Dashboard.css'
import { title } from 'process';
import CreateUser from '../components/CreateUser/CreateUser';
import { User } from '../types';
import axios from 'axios';

const { Header, Sider } = Layout;

const Dashboard: React.FC = () => {
  const [newUser, setNewUser] = useState<User | null>(null);

  const dispatch = useDispatch();

  useEffect(() => {
      const fetchData = async () => {
          try {
            await fetchUserFromAPI().then(userResponse => dispatch(setUser(userResponse)));
            await fetchProjectFromAPI().then(projectResponse => dispatch(setProject(projectResponse)));
        } catch (error) {
              console.error("Erro ao buscar dados da API:", error);
          }
      };

      fetchData();
  }, []);

  const [visible, setVisible] = useState(false);
  
  const showModal = (type : string) => {
    setVisible(true);
  };

  const handleOk = () => {
    setVisible(false);
    CreateUserByRandom();
  };
  const CreateUserByRandom = () => {
    if (newUser != null)
    {
      const requestBody : User = newUser;
      axios.post('http://localhost:5130/api/User', requestBody, {
          headers: {
              'Content-Type': 'application/json',
              'Access-Control-Allow-Origin': '*'
          }
      }).then(async response => {
        await fetchUserFromAPI().then(userResponse => dispatch(setUser(userResponse)));
      }).catch(error => {
          console.error('Error:', error);
      });
    }
  };

  const handleCreateUser = (value: User) => {
    setNewUser(value)
  };

  const handleCancel = () => {
    setVisible(false);
  };

  const HandleCreateUserOpenModal = () => {
    setVisible(true);
  }

  return (
    <Layout style={{ minHeight: '100vh' }}>
      <Header className="header">
        <div className="logo" />
        <h1 style={{ color: 'white' }}>Atlas Project</h1>
      </Header>
      <Layout>
        <Sider width={200} className="site-layout-background">
          <Menu
            mode="inline"
            defaultSelectedKeys={['1']}
            defaultOpenKeys={['sub1']}
            style={{ height: '100%', borderRight: 0 }}
          >
            <div className='breadcrumbs'>
              <h3>Dashboard</h3>
            </div>
            <Menu.Item key="1" icon={<LaptopOutlined />}>
              Projetos
            </Menu.Item>
            <Menu.Item onClick={HandleCreateUserOpenModal} key="2" icon={<UserOutlined />}>
              Criar Usuário Aleatóroio
            </Menu.Item>
          </Menu>
        </Sider>
        <div className='content'>
          <ListProjects OpenProjectDetail={showModal} />
          <ProjectForm  />
        </div>
        {visible && <Modal
          title="Criador de usuários aleatórios"
          visible={visible}
          onOk={handleOk}
          onCancel={handleCancel}
        >
          <CreateUser handleCreateUser={handleCreateUser}/>
        </Modal>}
      </Layout>
    </Layout>
  );
};

export default Dashboard;