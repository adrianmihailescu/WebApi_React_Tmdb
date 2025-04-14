export const login = async (username: string, password: string): Promise<string> => {
    return new Promise((resolve, reject) => {
      setTimeout(() => {
        if (username === 'admin' && password === 'password') {
          resolve('7ac12656d3b684fe06863b561c366c79a');
        } else {
          reject('Invalid credentials');
        }
      }, 1000); // simulate API delay
    });
  };
  