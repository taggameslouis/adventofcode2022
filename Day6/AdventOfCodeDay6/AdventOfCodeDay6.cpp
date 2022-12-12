#include <algorithm>
#include <fstream>
#include <vector>
#include <iostream>

using namespace std;

int findUniqueSetOfCharacters(ifstream& fin, int bufferSize)
{
    vector<char> buffer (bufferSize,0);
    int count = 0;

    while(!fin.eof())
    {
        fin.read(buffer.data(), bufferSize);
        count++;
        
        streamsize s = fin.gcount();
        if(s < bufferSize)
            break;

        sort(buffer.begin(), buffer.end());
        const auto dupesIt = std::adjacent_find(buffer.begin(), buffer.end());
        if (dupesIt == buffer.end())
        {
            return count + bufferSize - 1;
        }

        fin.seekg(count);
    }

    return -1;
}

int main(int argc, char* argv[])
{
    ifstream fin("input.txt", ifstream::binary);

    int startIndexOfPacket = findUniqueSetOfCharacters(fin, 4);
    cout << "[PacketSeek] Found at " << startIndexOfPacket << endl;
    
    return 0;
}
